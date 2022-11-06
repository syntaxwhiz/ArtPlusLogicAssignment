using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter up to 4 characters: ");
        string input_string = Console.ReadLine();

        string[] hex_codes = new string[4];
        for (int i = 0; i < 4; i++)
        {
            if (i < input_string.Length)
            {
                char character = input_string[i];
                string hex_val = ((int)character).ToString("X2");
                hex_codes[i] = "0x" + hex_val;
            }
            else
            {
                hex_codes[i] = "0x00";
            }
        }

        string[] binary_codes = new string[4];
        for (int i = 0; i < hex_codes.Length; i++)
        {
            string hex_val = hex_codes[i];
            int decimal_value = int.Parse(hex_val.Substring(2), System.Globalization.NumberStyles.HexNumber);
            string binary_code = Convert.ToString(decimal_value, 2).PadLeft(8, '0');
            binary_codes[i] = binary_code;
        }
        Array.Reverse(binary_codes);

        string[] Encoded = new string[32];
        string encoded_value = "";
        int index = 0;
        int string_index = 0;
        for (int iteration = 1; iteration <= 32; iteration++)
        {
            encoded_value += binary_codes[index][string_index];
            if (iteration % 8 == 0)
            {
                Encoded[iteration / 8 - 1] = encoded_value;
                encoded_value = "";
            }
            index = (index + 1) % 4;
            if (index == 0)
            {
                string_index += 1;
            }
        }

        Console.WriteLine("Hex codes:");
        foreach (string hex_val in hex_codes)
        {
            Console.Write(hex_val + " ");
        }
        Console.WriteLine();

        Console.WriteLine("Binary codes:");
        foreach (string binary_code in binary_codes)
        {
            Console.Write(binary_code + " ");
        }
        Console.WriteLine();

        Console.WriteLine("Encoded codes:");
        foreach (string encoded_code in Encoded)
        {
            Console.Write(encoded_code + " ");
        }
        Console.WriteLine();

        string concatenated_binary = string.Join("", Encoded);
        int decimal_code = Convert.ToInt32(concatenated_binary, 2);
        string hex_val_result = decimal_code.ToString("X8");
        Console.WriteLine("Hex code: 0x" + hex_val_result);

        Console.WriteLine("Decimal code: " + decimal_code);

        string binary = DecimalToBinaryTwosComplement(decimal_code);
        Console.WriteLine("binary_number: " + binary);

        string[] decoded_codes = new string[4];
        int decode_array_index = 0;
        foreach (char item in binary)
        {
            decoded_codes[decode_array_index] += item;
            decode_array_index = (decode_array_index + 1) % 4;
        }

        Console.WriteLine("decoded codes:");
        foreach (string decoded_code in decoded_codes)
        {
            Console.Write(decoded_code + " ");
        }
        Console.WriteLine();

        string[] hexa_codes = new string[4];
        for (int i = 0; i < decoded_codes.Length; i++)
        {
            int decimal_value = Convert.ToInt32(decoded_codes[i], 2);
            string hex_val = decimal_value.ToString("X2");
            hexa_codes[i] = "0x" + hex_val;
        }
        Array.Reverse(hexa_codes);

        Console.WriteLine("Hex codes (descending order):");
        foreach (string hex_val in hexa_codes)
        {
            Console.Write(hex_val + " ");
        }
        Console.WriteLine();

        string decoded_string = "";
        foreach (string hex_val in hexa_codes)
        {
            int decimal_code_value = Convert.ToInt32(hex_val.Substring(2), 16);
            if (decimal_code_value != 0)
            {
                decoded_string += (char)decimal_code_value;
            }
        }
        Console.WriteLine("Decoded string: " + decoded_string);
    }
}
