﻿using System;
using System.Security;
using System.Runtime.InteropServices;

namespace Secure_String
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            using (var ss = new SecureString())
            {
                Console.Write("Please enter password: ");

                while (true)
                {
                    var cki = Console.ReadKey(true);

                    if (cki.Key == ConsoleKey.Enter) break;

                    // Присоединить символы пароля в конец SecureString
                    ss.AppendChar(cki.KeyChar);

                    Console.Write("*");
                }

                Console.WriteLine();

                // Пароль введен, отобразим его для демонстрационных целей
                DisplaySecureString(ss);
            }
            // После 'using' SecureString обрабатывается методом Disposed,
            // поэтому никаких конфиденциальных данных в памяти нет
        }

        // Этот метод небезопасен, потому что обращается к неуправляемой памяти
        private unsafe static void DisplaySecureString(SecureString ss)
        {
            Char* pc = null;

            try
            {
                // Дешифрование SecureString в буфер неуправляемой памяти
                pc = (Char*)Marshal.SecureStringToCoTaskMemUnicode(ss);

                // Доступ к буферу неуправляемой памяти,
                // который хранит дешифрованную версию SecureString
                for (var index = 0; pc[index] != 0; index++)
                {
                    Console.Write(pc[index]);
                }
            }
            finally
            {
                // Обеспечиваем обнуление и освобождение буфера неуправляемой памяти,
                // который хранит расшифрованные символы SecureString
                if (pc != null)
                {
                    Marshal.ZeroFreeCoTaskMemUnicode((IntPtr)pc);
                }
            }
        }
    }
}