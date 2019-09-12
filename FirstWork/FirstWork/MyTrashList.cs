using System;
using System.Collections;
using System.Collections.Generic;

namespace MySpecialList
{
    public class MyTrashList : IEnumerable, IEnumerator
    {
        /// <summary>
        /// Объект перечисления списка
        /// </summary>
        private readonly MyTrashListNumerator _trashListNumerator;

        public MyTrashList()
        {
            _trashListNumerator.NumenatorArray = new object[8];
            _trashListNumerator.NumenatorIterator = 0;
            _trashListNumerator = new MyTrashListNumerator();
        }

        public MyTrashList(int count)
        {
            _trashListNumerator.NumenatorArray = new object[count];
            _trashListNumerator.NumenatorIterator = 0;
            _trashListNumerator = new MyTrashListNumerator(count);
        }

        public MyTrashList(IEnumerable<object> collection)
        {
            IEnumerator enumerator = collection.GetEnumerator();
            var countofCollection = GetCountOfCollection(collection);
            _trashListNumerator.NumenatorArray = new object[countofCollection];
            for (var i = 0; i < countofCollection; i++)
            {
                _trashListNumerator.NumenatorArray[i] = enumerator.Current;
                enumerator.MoveNext();
            }

            _trashListNumerator.NumenatorIterator = 0;
        }

        /// <summary>
        ///     Длина списка
        /// </summary>
        public int Length => _trashListNumerator.NumenatorIterator;

        /// <summary>
        ///     Реальная текущая вместимость
        /// </summary>
        public int Capacity => _trashListNumerator.NumenatorArray.Length;

        public IEnumerator GetEnumerator()
        {
            return this._trashListNumerator; // Подумать точно так реализовано или нет
        }

        /// <summary>
        ///     Возвращает текущий элемент
        /// </summary>
        public object Current => _trashListNumerator.NumenatorArray[_trashListNumerator.NumenatorIterator];

        public bool MoveNext()
        {
            if (_trashListNumerator.NumenatorIterator == _trashListNumerator.NumenatorArray.Length) return false;

            _trashListNumerator.NumenatorIterator++;
            return true;
        }

        /// <summary>
        ///     Сбрасывает индекс-счетчик на начало
        /// </summary>
        public void Reset()
        {
            _trashListNumerator.NumenatorIterator = 0;
        }

        /// <summary>
        ///     Добавдение нового элемента в коллекцию
        /// </summary>
        /// <param name="newObject">Новый объект</param>
        public void Add(object newObject)
        {
            if (_trashListNumerator.NumenatorIterator < _trashListNumerator.NumenatorArray.Length - 1)
            {
                _trashListNumerator.NumenatorArray[_trashListNumerator.NumenatorIterator] = newObject;
                _trashListNumerator.NumenatorIterator++;
            }
            else
            {
                var trashList = new object[_trashListNumerator.NumenatorArray.Length * 2];
                _trashListNumerator.NumenatorArray.CopyTo(trashList, 0);
                _trashListNumerator.NumenatorArray[_trashListNumerator.NumenatorIterator] = newObject;
                _trashListNumerator.NumenatorIterator++;
            }
        }

        /// <summary>
        ///     Добавить коллекцию к коллекции
        /// </summary>
        /// <param name="collection">Дополнительная коллекция</param>
        public void AddRange(IEnumerable<object> collection)
        {
            var countNewCollection = GetCountOfCollection(collection);
            var newCollection = new object[countNewCollection + _trashListNumerator.NumenatorArray.Length];
            for (var i = 0; i < _trashListNumerator.NumenatorArray.Length; i++) newCollection[i] = _trashListNumerator.NumenatorArray[i];
            for (var i = _trashListNumerator.NumenatorArray.Length; i < countNewCollection; i++) newCollection[i] = _trashListNumerator.NumenatorArray[i];
        }

        /// <summary>
        ///     Удалить элемент на указанном индексе
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        /// <returns>Возвращает true если выполнилось</returns>
        public bool Remove(int index)
        {
            try
            {
                for (var i = index; i < _trashListNumerator.NumenatorArray.Length - 1; i++) _trashListNumerator.NumenatorArray[i] = _trashListNumerator.NumenatorArray[i + 1];

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Вставляет элемент на нужный индекс
        /// </summary>
        /// <param name="obj">Объект для вставки</param>
        /// <param name="index">Индекс для вставки</param>
        /// <returns>Возвращает true если выполнилось</returns>
        public bool Insert(object obj, int index)
        {
            if (index == _trashListNumerator.NumenatorArray.Length)
            {
                Add(obj);
                return true;
            }

            if (_trashListNumerator.NumenatorIterator == _trashListNumerator.NumenatorArray.Length)
            {
                var newCollection = new object[_trashListNumerator.NumenatorArray.Length + 1];
                for (var i = 0; i < index; i++) newCollection[i] = _trashListNumerator.NumenatorArray[i];

                newCollection[index] = obj;

                for (var i = index + 1; i < newCollection.Length; i++) newCollection[i] = _trashListNumerator.NumenatorArray[i + 1];

                return true;
            }

            return false;
        }

        private int GetCountOfCollection(IEnumerable<object> collection)
        {
            var countOfElements = 0;
            IEnumerator enumerator = collection.GetEnumerator();
            object prevObject = null;
            do
            {
                prevObject = enumerator.Current;
                countOfElements += 1;
            } while (enumerator.Current != null && enumerator.Current.Equals(prevObject));

            return countOfElements;
        }

        private class MyTrashListNumerator : IEnumerator
        {
            public MyTrashListNumerator()
            {
                NumenatorIterator = 0;
                NumenatorArray = new object[8];
            }

            public MyTrashListNumerator(int count)
            {
                NumenatorIterator = 0;
                NumenatorArray = new object[count];
            }

            public MyTrashListNumerator(object[] array)
            {
                NumenatorIterator = 0;
                NumenatorArray = array;
            }

            public bool MoveNext()
            {
                if (NumenatorArray.Length == NumenatorIterator)
                {
                    return false;
                }

                NumenatorIterator++;
                return true;
            }

            public void Reset()
            {
                NumenatorIterator = 0;
            }

            public object[] NumenatorArray { get; set; }

            public int NumenatorIterator { get; set; }

            public object Current => NumenatorArray[NumenatorIterator];
        }
    }
}