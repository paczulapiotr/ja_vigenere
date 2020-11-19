using System;
using System.Runtime.InteropServices;

namespace GaussFilter.Algorithm
{
    public class VigenereCipher
    {
        [DllImport("Vigenere.ASM.dll", EntryPoint = "cipher")]
        private static extern unsafe int VigenereCipherAsm(char* text, char* key, char* encrypted, int textLength, int keyLength);

        public unsafe string Encrypt(string text, string key)
        {
            string encryptedText;
            var textLength = text.Length;
            var keyLength = key.Length;
            var textCharArray = text.ToCharArray();
            var keyCharArray = key.ToCharArray();
            fixed (char* textPtr = textCharArray)
            fixed (char* keyPtr = keyCharArray)
            fixed (char* encryptedPtr = keyCharArray)
            {
                VigenereCipherAsm(textPtr, keyPtr, encryptedPtr, textLength, keyLength);
                encryptedText = Marshal.PtrToStringAnsi((IntPtr)encryptedPtr);
            }

            return encryptedText;

        }
    }
}
