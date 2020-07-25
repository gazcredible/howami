using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace serialiser_00
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var data = new Dictionary<DateTime, UserRecord>();

            var filename = "playerInfo.dat";

            if (File.Exists(filename) == true)
            {
                var stream = File.Open(filename, FileMode.Open);
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    data = bf.Deserialize(stream) as Dictionary<DateTime, UserRecord>;

                    Console.WriteLine("PersistentData - loaded:" + filename);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("PersistentData - is out of date");
                    data = null;

                    Console.WriteLine("PersistentData - can't load:" + filename +"\n"+ex);
                }

                stream.Close();
            }
        }
    }
}