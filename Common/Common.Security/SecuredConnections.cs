using Common.Security.CryptoData;
using Common.Security.Registry;
using Common.Security.Security;

namespace Common.Security
{
    public class SecuredConnections
    {
        public static string GetFromRegistry(string keyName)
        {
            var cscs = new StringCryptoData
            {
                CryptoTransformFactory = new CryptoTransformFactory
                {
                    AlgorithmFactory = new AlgorithmFactory
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.TripleDes
                    }
                },
                Storage = new RegistrySecurityStorage
                {
                    RegistryStorageRoot = RegistryStorageRoot.LocalMachine,
                    SecureValueName = "ConnectionString"
                }
            };

            return cscs.GetValue(keyName);
        }

    }

}
