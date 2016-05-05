using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfTcpDemo
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ClientsService : IClientCollectionService
    {
        private List<Client> _clients = new List<Client>();
        private Dictionary<string, List<string>> _additionalClientInfo = new Dictionary<string, List<string>>();

        public bool AddClient(string clientName)
        {
            if (String.IsNullOrEmpty(clientName)) throw new ArgumentNullException();

            if (!_clients.Exists(c => c.Name == clientName))
            {
                Client newClient = new Client() { Name = clientName, LastChanges = DateTime.Now };
                _clients.Add(newClient);

                var defaultClientInfo = new List<string>() { clientName };
                _additionalClientInfo.Add(clientName, defaultClientInfo);

                return true;
            }

            return false;
        }

        public bool AddInfoToClient(string clientName, string additionalInfo)
        {
            if (String.IsNullOrEmpty(clientName) || String.IsNullOrEmpty(additionalInfo))
                throw new ArgumentNullException();

            if (_additionalClientInfo.ContainsKey(clientName))
            {
                _additionalClientInfo[clientName].Add(additionalInfo);

                var client = _clients.Find(c => c.Name == clientName);
                client.LastChanges = DateTime.Now;

                return true;
            }
            
            return false;
        }

        public Client GetClient(string clientName)
        {
            if (String.IsNullOrEmpty(clientName)) throw new ArgumentNullException();

            return _clients.Find(c => c.Name == clientName);
        }

        public bool RemoveClient(string clientName)
        {
            if (String.IsNullOrEmpty(clientName)) throw new ArgumentNullException();

            Client client;
            if ((client = _clients.Find(c => c.Name == clientName)) != null)
            {
                _clients.Remove(client);

                _additionalClientInfo[clientName].Clear();
                _additionalClientInfo.Remove(clientName);

                return true;
            }

            return false;
        }

        public List<string> GetClientInfo(string clientName)
        {
            if (String.IsNullOrEmpty(clientName)) throw new ArgumentNullException();

            return _additionalClientInfo[clientName];
        }
    }
}
