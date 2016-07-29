using DS.Kids.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Kids.Model
{
    public class ParceiroSingleton : IParceiro
    {
        private ParceiroSingleton() { }
        private static ParceiroSingleton _instance;
        private static object _lock = new object();
        private int _id;

        public static ParceiroSingleton Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new ParceiroSingleton();

                    return _instance;
                }
            }
        }

        public void Inserir(int id)
        {
            this._id = id;
        }

        public int Obter()
        {
            return this._id;
        }
    }
}
