using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CipherEncoder
{
    public partial class CodeCipher : Form
    {
        public CodeCipher()
        {
            InitializeComponent();
            txtPlaintext.Focus();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            int offsetKey;
            offsetKey = random_Generator_Key();
            encryptMessage(offsetKey);
        }

        private int random_Generator_Key()
        {
            int offset_Factor = 0;          // This generates a random number between 33 and 126 as specified.
            string encryptionKey = "";      // Set up an empty encryptionKey, ready to store the random 8 characters.
            Random rnd = new Random();

            for (int x = 0; x < 8; x++)
            {
                
                int randomKey = rnd.Next(33, 126);
                char currentKey = (char)randomKey;
                encryptionKey = encryptionKey + currentKey;
            }

            txtKey.Text = encryptionKey;

            int keyTotal = 0;    // Sets up running total as 0.

            foreach (char convertChar in encryptionKey)           // Sets current Character looked at as 0.
            {
                // Calculate the new offset factor to use...
                int numASCII = (int)convertChar;
                // keeps a running total here to perform the maths on later.
                keyTotal += numASCII;
            }

            // divide the total by 8 and round it down. I used math.floor() to do this.
            decimal offset1 = (keyTotal / 8);
            offset1 = Math.Floor(offset1);
            offset_Factor = ((int)offset1 - 32);

            return offset_Factor;
        }

        private void encryptMessage(int offset_Factor)
        {
            string encryptedText = "";      // Set up 'encrypted' variable to hold all of the encrypted message.
            string plainText = txtPlaintext.Text;
            int maxChar = plainText.Length;
            char encryptChar;


            foreach (char currentChar in plainText)
            {
                int numASCII = (int)currentChar;

                //if the ASCII value is 32, it is a [space] so keep it the same.
                if (numASCII == 32)
                    encryptChar = (char)numASCII;
                else
                {
                    // add the ASCII number to the offset factor.
                    int newNum = numASCII + offset_Factor;
                    //if the number is greater than 126, subtract 94 to make it a valid ASCII number.
                    if (newNum > 126)
                    {
                        newNum = newNum - 94;
                        encryptChar = (char)newNum;
                    }
                    else
                    {
                        encryptChar = (char)newNum;
                    }
                }

                // insert the newly encrypted character into the 'Encrypted' string.
                encryptedText = encryptedText + encryptChar;

                // increase the index of your 'slice' for the file being encrypted.
                // this moves it on to the next character to be encrypted.

                txtCiphertext.Text = encryptedText;
            }
        }

        

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            int offsetKey;
            offsetKey = decrypt_Key_Generator();
            decryptMessage(offsetKey);
        }

        private int decrypt_Key_Generator()
        {
            int decrypt_Factor;         // Sets current Character as 0.
            int decryptKeyTotal = 0;

            string decryptionKey = txtKey.Text;

            foreach (char convertChar in decryptionKey)
            {
                // Calculate the new offset factor to use...
                int numASCII = (int)convertChar;
                // keeps a running total here to perform the maths on later.
                decryptKeyTotal += numASCII;
            }

            double offset1 = decryptKeyTotal / 8;
            offset1 = Math.Floor(offset1);
            decrypt_Factor = ((int)offset1 - 32);

            return decrypt_Factor;
        }

        private void decryptMessage(int offset_Factor)
        {
            string decryptedText = "";      // Set up 'encrypted' variable to hold all of the encrypted message.
            string cipherText = txtCiphertext.Text;
            int maxChar = cipherText.Length;
            char decryptChar;


            foreach (char currentChar in cipherText)
            {
                int numASCII = (int)currentChar;

                //if the ASCII value is 32, it is a [space] so keep it the same.
                if (numASCII == 32)
                    decryptChar = (char)numASCII;
                else
                {
                    // add the ASCII number to the offset factor.
                    int newNum = numASCII - offset_Factor;
                    //if the number is greater than 126, subtract 94 to make it a valid ASCII number.
                    if (newNum > 126)
                    {
                        newNum = newNum - 94;
                        decryptChar = (char)newNum;
                    }
                    if (newNum < 33)
                    {
                        newNum = newNum + 94;
                        decryptChar = (char)newNum;
                    }
                    else
                    {
                        decryptChar = (char)newNum;
                    }
                }

                // insert the newly encrypted character into the 'Encrypted' string.
                decryptedText = decryptedText + decryptChar;

                // increase the index of your 'slice' for the file being encrypted.
                // this moves it on to the next character to be encrypted.

                txtPlaintext.Text = decryptedText;
            }
        }

        private void btnSuper_Click(object sender, EventArgs e)
        {
            //int offsetKey;
            //offsetKey = random_Generator_Key();
            specialEncryption();
        }

        private void specialEncryption()
        {
            int blockCounter = 0;       // Sets up a blockCounter
            string encryptedText = "";

            string originalText = txtPlaintext.Text;

            foreach (char testChar in originalText)               // For each character in your message.
            {
                if (((int)testChar) == 32)                        // Tests to see if current character is a space.
                {
                    encryptedText = encryptedText + "";           // If a space, then enter nothing into the message.
                }
                else
                {
                    encryptedText = encryptedText + testChar;     // If not a space then add in the character to new message   
                    blockCounter += 1;                            // Add 1 to blockCounter variable.
                }
                if (blockCounter == 5)                            // When blockCounter hits 5.
                {
                    encryptedText = encryptedText + " ";          // Then it puts a space in and starts a new block.
                    blockCounter = 0;                             // Resets blockCounter for next block of 5.
                }
            }

            txtCiphertext.Text = encryptedText;

        }
    }
}
