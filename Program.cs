using System;
using System.Security.Cryptography;
using System.Text;

namespace otpsceneriosAndCaptha
{
    class Program
    {

        
            static void Main(string[] args)
            {
                // Generate an OTP
                string otp = GenerateOTP(6);
                Console.WriteLine($"Your OTP is: {otp}");

                // Set OTP validity duration (e.g., 60 seconds)
                DateTime otpExpiryTime = DateTime.Now.AddSeconds(60);

                // Prompt the user to enter the OTP
                Console.WriteLine("Please enter the OTP:");

                // Verify the OTP
                while (DateTime.Now < otpExpiryTime)
                {
                    string Input = Console.ReadLine();

                    if (Input == otp)
                    {
                        Console.WriteLine("OTP verified successfully!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid OTP. Please try again:");
                    }
                }

                if (DateTime.Now >= otpExpiryTime)
                {
                    Console.WriteLine("OTP has expired.");
                }

            // writetten here for captha generation
            string captcha = GenerateCAPTCHA(6);
            Console.WriteLine($"Your CAPTCHA is: {captcha}");

            // Prompt the user to enter the CAPTCHA
            Console.WriteLine("Please enter the CAPTCHA:");

            // Verify the CAPTCHA
            string userInput = Console.ReadLine();
            if (userInput == captcha)
            {
                Console.WriteLine("CAPTCHA verified successfully!");
            }
            else
            {
                Console.WriteLine("Invalid CAPTCHA. Please try again.");
            }
        }

            static string GenerateOTP(int length)
            {
                const string validChars = "0123456789,abcdefghikl,@$&*";
                StringBuilder res = new StringBuilder();
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                    byte[] uintBuffer = new byte[sizeof(uint)];

                    while (length-- > 0)
                    {
                        rng.GetBytes(uintBuffer);
                        uint num = BitConverter.ToUInt32(uintBuffer, 0);
                        res.Append(validChars[(int)(num % (uint)validChars.Length)]);
                    }
                }

                return res.ToString();
            }
        /// generate for captha code
        /// 
        static string GenerateCAPTCHA(int length)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(validChars[(int)(num % (uint)validChars.Length)]);
                }
            }

            return res.ToString();
        }
    }
    }


