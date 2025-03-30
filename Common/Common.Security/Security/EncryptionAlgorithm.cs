namespace Common.Security.Security
{
    /// <summary>
    /// Поддерживаемые алгоритмы шифрования
    /// </summary>
    public enum EncryptionAlgorithm
    {
        Undefined = 0,
        Des = 1, // DES
        Rc2 = 2, // RC2
        Rijndael = 3, // Rijndael
        TripleDes = 4 // Triple DES
    }

}
