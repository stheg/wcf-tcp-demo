using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfTcpDemo
{
    [ServiceContract(ProtectionLevel = System.Net.Security.ProtectionLevel.None)]
    public interface IClientCollectionService
    {
        [OperationContract]
        bool AddClient(string clientName);

        [OperationContract]
        bool RemoveClient(string clientName);

        [OperationContract]
        bool AddInfoToClient(string clientName, string additionalInfo);

        [OperationContract]
        Client GetClient(string clientName);

        [OperationContract]
        List<string> GetClientInfo(string clientName);
    }

    [DataContract]
    public class Client
    {
        string _name;

        [DataMember]
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        [DataMember]
        public DateTime LastChanges
        {
            get
            {
                return _lastChanges;
            }

            set
            {
                _lastChanges = value;
            }
        }

        DateTime _lastChanges;
    }
}
