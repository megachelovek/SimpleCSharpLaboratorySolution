using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MySpecialList
{
    public class DynamicArray<T> : IEnumerable<T>, IEnumerator
    {
        /// <summary>
        ///     Объект перечисления списка
        /// </summary>
        private readonly DynamicArrayNumerator<T> _trashListNumerator;

        public DynamicArray()
        {
            _trashListNumerator = new DynamicArrayNumerator<T>();
            _trashListNumerator.NumenatorIterator = 0;
        }

        public DynamicArray(int count)
        {
            _trashListNumerator = new DynamicArrayNumerator<T>(count);
            _trashListNumerator.NumenatorIterator = 0;
        }

        public DynamicArray(IEnumerable<object> collection)
        {
            IEnumerator enumerator = collection.GetEnumerator();
            enumerator.Reset();
            var countOfCollection = GetCountOfCollection(collection);
            _trashListNumerator = new DynamicArrayNumerator<T>(countOfCollection);
            _trashListNumerator.NumenatorArray = new object[countOfCollection];
            for (var i = 0; i < countOfCollection; i++)
            {
                enumerator.MoveNext();
                _trashListNumerator.NumenatorArray[i] = enumerator.Current;
            }
        }

        /// <summary>
        ///     Длина списка
        /// </summary>
        public int Length => _trashListNumerator.NumenatorIterator;

        /// <summary>
        ///     Реальная текущая вместимость
        /// </summary>
        public int Capacity => _trashListNumerator.NumenatorArray.Length + 1;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _trashListNumerator;
        }

        public IEnumerator GetEnumerator()
        {
            return _trashListNumerator; // Подумать точно так реализовано или нет
        }

        /// <summary>
        ///     Возвращает текущий элемент
        /// </summary>
        public object Current => _trashListNumerator.Current;

        public bool MoveNext()
        {
            return _trashListNumerator.MoveNext();
        }


        /// <summary>
        ///     Сбрасывает индекс-счетчик на начало
        /// </summary>
        public void Reset()
        {
            _trashListNumerator.Reset();
        }

        /// <summary>
        ///     Добавдение нового элемента в коллекцию
        /// </summary>
        /// <param name="newObject">Новый объект</param>
        public void Add(object newObject)
        {
            if (_trashListNumerator.countOfAddableElements < _trashListNumerator.NumenatorArray.Length - 1)
            {
                _trashListNumerator.NumenatorArray[_trashListNumerator.countOfAddableElements] = newObject;
                _trashListNumerator.NumenatorIterator++;
                _trashListNumerator.countOfAddableElements++;
            }
            else
            {
                var trashList = new object[_trashListNumerator.NumenatorArray.Length * 2];
                _trashListNumerator.NumenatorArray.CopyTo(trashList, 0);
                _trashListNumerator.NumenatorArray = trashList;
                _trashListNumerator.NumenatorArray[_trashListNumerator.countOfAddableElements] = newObject;
                _trashListNumerator.NumenatorIterator++;
                _trashListNumerator.countOfAddableElements++;
            }
        }

        internal int[] Where(Expression expressionIn)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Добавить коллекцию к коллекции
        /// </summary>
        /// <param name="collection">Дополнительная коллекция</param>
        public void AddRange(IEnumerable<object> collection)
        {
            var countNewCollection = GetCountOfCollection(collection);
            var newCollection = new object[countNewCollection + _trashListNumerator.NumenatorArray.Length];
            for (var i = 0; i < _trashListNumerator.NumenatorArray.Length; i++)
                newCollection[i] = _trashListNumerator.NumenatorArray[i];
            var enumerator = collection.GetEnumerator();
            enumerator.Reset();
            var index = _trashListNumerator.NumenatorArray.Length;
            while (enumerator.MoveNext())
            {
                newCollection[index] = enumerator.Current;
                index++;
            }

            _trashListNumerator.NumenatorArray = newCollection;
            _trashListNumerator.countOfAddableElements =
                _trashListNumerator.countOfAddableElements + countNewCollection;
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
                var newArray = new object[_trashListNumerator.NumenatorArray.Length - 1];
                for (var i = 0; i < index; i++) newArray[i] = _trashListNumerator.NumenatorArray[i];
                for (var i = index; i < _trashListNumerator.NumenatorArray.Length - 1; i++)
                    newArray[i] = _trashListNumerator.NumenatorArray[i + 1];
                _trashListNumerator.NumenatorArray = newArray;
                _trashListNumerator.countOfAddableElements--;
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
                _trashListNumerator.countOfAddableElements++;
                return true;
            }

            if (_trashListNumerator.NumenatorIterator == _trashListNumerator.NumenatorArray.Length)
            {
                var newCollection = new object[_trashListNumerator.NumenatorArray.Length + 1];
                for (var i = 0; i < index; i++) newCollection[i] = _trashListNumerator.NumenatorArray[i];

                newCollection[index] = obj;

                for (var i = index + 1; i < newCollection.Length; i++)
                    newCollection[i] = _trashListNumerator.NumenatorArray[i - 1];
                _trashListNumerator.NumenatorArray = newCollection;
                _trashListNumerator.countOfAddableElements++;
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Получение количества элементов в коллекции
        /// </summary>
        /// <param name="collection">Коллекция для проверки</param>
        /// <returns>Количество элементов</returns>
        private int GetCountOfCollection(IEnumerable<object> collection)
        {
            var countOfElements = 0;
            IEnumerator enumerator = collection.GetEnumerator();
            enumerator.Reset();
            while (enumerator.MoveNext()) countOfElements++;

            return countOfElements;
        }

        private class DynamicArrayNumerator<T> : IEnumerator<T>
        {
            public DynamicArrayNumerator()
            {
                NumenatorIterator = 0;
                NumenatorArray = new object[8];
                countOfAddableElements = 0;
            }

            public DynamicArrayNumerator(int count)
            {
                NumenatorIterator = 0;
                NumenatorArray = new object[count];
                countOfAddableElements = 0;
            }

            public DynamicArrayNumerator(object[] array)
            {
                NumenatorIterator = 0;
                NumenatorArray = array;
                countOfAddableElements = 0;
            }

            public int countOfAddableElements { get; set; }

            public object[] NumenatorArray { get; set; }

            public int NumenatorIterator { get; set; }

            public T Current => (T) NumenatorArray[NumenatorIterator];

            public bool MoveNext()
            {
                if (NumenatorArray.Length - 1 == NumenatorIterator || NumenatorArray[NumenatorIterator + 1] == null)
                    return false;

                NumenatorIterator++;
                return true;
            }

            public void Reset()
            {
                NumenatorIterator = 0;
            }

            object IEnumerator.Current => Current;


            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}