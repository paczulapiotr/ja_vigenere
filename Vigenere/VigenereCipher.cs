using System;
using System.Runtime.InteropServices;

namespace Vigenere
{
    public class VigenereCipher
    {
        [DllImport("Vigenere.ASM.dll", EntryPoint = "encrypt")]
        private static extern unsafe int VigenereCipherAsm(char* text, char* key, char* encrypted, int textLength, int keyLength);

        public unsafe string Encrypt(string text, string key)
        {
            key = key.Trim().ToUpper();
            string encryptedText;
            var textLength = text.Length;
            var keyLength = key.Length;
            var textCharArray = text.ToCharArray();
            var keyCharArray = key.ToCharArray();
            fixed (char* textPtr = textCharArray)
            fixed (char* keyPtr = keyCharArray)
            fixed (char* encryptedPtr = new char[textLength])
            {
                VigenereCipherAsm(textPtr, keyPtr, encryptedPtr, textLength, keyLength);
                encryptedText = Marshal.PtrToStringAnsi((IntPtr)encryptedPtr);
            }

            return encryptedText;

        }
    }
}
