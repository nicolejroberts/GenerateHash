// See https://aka.ms/new-console-template for more information

using System;
using System.Security.Cryptography;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter the JSON body:");
        string jsonData = ReadMultilineInput();

        Console.WriteLine("Enter the hash secret:");
        string hashSecret = Console.ReadLine().Trim();

        string signature = CreateSignatureHash(jsonData, hashSecret);

        Console.WriteLine("\nComputed Signature:");
        Console.WriteLine(signature);
    }
    
    private static string ReadMultilineInput()
    {
        StringBuilder jsonData = new StringBuilder();
        string line;
        while (!string.IsNullOrEmpty(line = Console.ReadLine()))
        {
            jsonData.AppendLine(line);
        }
        return jsonData.ToString().Trim();
    }

    private static string CreateSignatureHash(string data, string hashSecret)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(hashSecret);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);

        using (HMACSHA256 hmac = new HMACSHA256(keyBytes))
        {
            byte[] hmacBytes = hmac.ComputeHash(dataBytes);
            return Convert.ToBase64String(hmacBytes);
        }
    }
}