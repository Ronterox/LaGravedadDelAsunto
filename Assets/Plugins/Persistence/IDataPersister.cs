using UnityEngine;

namespace Plugins.Persistence
{
    /// <summary>
    /// Classes that implement this interface should have an serialized instance of DataSettings to register through.
    /// </summary>
    public interface IDataPersister
    {
        /// <summary>
        /// Get Data Settings
        /// </summary>
        /// <returns></returns>
        DataSettings GetDataSettings();

        /// <summary>
        /// Set Data Settings
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="persistenceType"></param>
        void SetDataSettings(ushort dataId, DataSettings.PersistenceType persistenceType);

        /// <summary>
        /// Save Data
        /// </summary>
        /// <returns></returns>
        Data SaveData();

        /// <summary>
        /// Load Data
        /// </summary>
        /// <param name="data"></param>
        void LoadData(Data data);
    }

    [System.Serializable]
    public class DataSettings
    {
        public enum PersistenceType
        {
            DoNotPersist,
            ReadOnly,
            WriteOnly,
            ReadWrite,
        }

        /// <summary>
        /// Support variables
        /// </summary>
        public ushort dataId = (ushort) System.Guid.NewGuid().GetHashCode();
        public PersistenceType type = PersistenceType.ReadWrite;

        public override string ToString() => dataId + " " + type;

        public void GenerateId(string text) => dataId = (ushort) text.GetHashCode();

        public void GenerateId(GameObject gameObject) => GenerateId($"{gameObject.scene.name}_{gameObject.name}");
    }

    [System.Serializable]
    public class Data
    {

    }

    [System.Serializable]
    public class Data<T> : Data
    {
        public T value;

        public Data(T value) => this.value = value;
    }

    [System.Serializable]
    public class Data<T0, T1> : Data
    {
        public T0 value0;
        public T1 value1;

        public Data(T0 value0, T1 value1)
        {
            this.value0 = value0;
            this.value1 = value1;
        }
    }

    [System.Serializable]
    public class Data<T0, T1, T2> : Data
    {
        public T0 value0;
        public T1 value1;
        public T2 value2;

        public Data(T0 value0, T1 value1, T2 value2)
        {
            this.value0 = value0;
            this.value1 = value1;
            this.value2 = value2;
        }
    }

    [System.Serializable]
    public class Data<T0, T1, T2, T3> : Data
    {
        public T0 value0;
        public T1 value1;
        public T2 value2;
        public T3 value3;

        public Data(T0 value0, T1 value1, T2 value2, T3 value3)
        {
            this.value0 = value0;
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
        }
    }

    [System.Serializable]
    public class Data<T0, T1, T2, T3, T4> : Data
    {
        public T0 value0;
        public T1 value1;
        public T2 value2;
        public T3 value3;
        public T4 value4;

        public Data(T0 value0, T1 value1, T2 value2, T3 value3, T4 value4)
        {
            this.value0 = value0;
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
            this.value4 = value4;
        }
    }
}