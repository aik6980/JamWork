namespace Util
{
    class Singleton<T> where T: class, new()
    {
        static T m_sSingleton = null; 
        public static T Instance()
        {
            if(m_sSingleton == null)
            {
                m_sSingleton = new T();
            }

            return m_sSingleton;
        }
    };
}