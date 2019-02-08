#region License
// ====================================================
// Z00bfuscator Copyright(C) 2013-2019 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;

namespace Z00bfuscator.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try {
                while (true) {
                    Console.Write("Enter your password: ");
                    string read = Console.ReadLine();

                    bool flag = true;

                    if (!string.IsNullOrEmpty(read)) {
                        if (ulong.TryParse(read.Trim(), out ulong input)) {
                            if (input == 0123456789) {
                                Console.WriteLine("Access granted!");
                                break;
                            } else {
                                Console.WriteLine("Access denied!");
                                break;
                            }
                        } else {
                            flag = false;
                        }
                    } else {
                        flag = false;
                    }

                    if (!flag) {
                        Console.WriteLine("Input format must be in integer format and can not be null or empty!");
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine($"[Program::Main()]: Unexpected condition handled: {ex.ToString()}");
            }

            Console.WriteLine("Reached end of the application!");
        }
    }
}
