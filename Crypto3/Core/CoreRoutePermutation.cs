using System;
using System.Collections.Generic;
using System.Text;

// This is an open source non-commercial project. Dear PVS-Studio, please check it.

// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

namespace Crypto3.Core
{
    class CoreRoutePermutation
    {
        string strForEncrypt { get; set; }

        const int _row = 4;

        int _column
        {
            get
            {
                if (strForEncrypt != null)
                    return strForEncrypt.Length % _row == 0 ? strForEncrypt.Length / _row : (strForEncrypt.Length / _row) + 1;
                else
                    throw new Exception("Что-то со строкой для шифрования");
            }
        }
        static string str;
        public string Encrypt(string strForEncrypt)
        {
            this.strForEncrypt = strForEncrypt;
            for (int i = 0; i < _row * _column - this.strForEncrypt.Length; i++)
            {
                strForEncrypt = this.strForEncrypt + ' ';
            }
            char[,] matrix = new char[_row, _column];


            bool verticalOrHorizontal = true, downOrUp = true, rightOrLeft = true;
            int iterationVertical = _row, iterationHorizontal = _column - 1;
            int y = 0, x = 0;
            for (int indexSymbol = 0; indexSymbol < strForEncrypt.Length;)
            {
                if (verticalOrHorizontal && downOrUp)
                {
                    for (int i = 0; i < iterationVertical; i++)
                    {
                        matrix[y, x] = strForEncrypt[indexSymbol];
                        indexSymbol++;
                        y++;

                    }
                    y--;
                    x++;
                    iterationVertical--;
                    verticalOrHorizontal = !verticalOrHorizontal;
                    downOrUp = !downOrUp;
                }
                else if (!verticalOrHorizontal && rightOrLeft)
                {
                    for (int i = 0; i < iterationHorizontal; i++)
                    {
                        matrix[y, x] = strForEncrypt[indexSymbol];
                        indexSymbol++;
                        x++;

                    }
                    x--;
                    y--;
                    iterationHorizontal--;
                    verticalOrHorizontal = !verticalOrHorizontal;
                    rightOrLeft = !rightOrLeft;
                }
                else if (verticalOrHorizontal && !downOrUp)
                {
                    for (int i = 0; i < iterationVertical; i++)
                    {
                        matrix[y, x] = strForEncrypt[indexSymbol];
                        indexSymbol++;
                        y--;

                    }
                    y++;
                    x--;
                    iterationVertical--;
                    verticalOrHorizontal = !verticalOrHorizontal;
                    downOrUp = !downOrUp;
                }
                else if (!verticalOrHorizontal && !rightOrLeft)
                {
                    for (int i = 0; i < iterationHorizontal; i++)
                    {
                        matrix[y, x] = strForEncrypt[indexSymbol];
                        indexSymbol++;
                        x--;

                    }
                    x++;
                    y++;
                    iterationHorizontal--;
                    verticalOrHorizontal = !verticalOrHorizontal;
                    rightOrLeft = !rightOrLeft;
                }
            }
            PrintArray(matrix);
            return retStr(matrix);
        }
        public void PrintArray(char[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public int[,] GetArray(string strEncrypt)
        {
            this.strForEncrypt = strEncrypt;
            var a = new int[_row, _column];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] = 1 + i * _column + i % 2 * (_column - 2 * j - 1) + j;
                }
            }
            return a;
        }
        private string retStr(char[,] matrix)
        {
            StringBuilder cryptoStr = new StringBuilder();
            for (int column = 0; column < matrix.GetLength(0); column++)
            {
                for (int row = 0; row < matrix.GetLength(1); row++)
                {
                    cryptoStr.Append(matrix[column, row]);
                }
            }
            return cryptoStr.ToString();
        }
        public string Decrypt(string strForDecrypt)
        {
            this.strForEncrypt = strForEncrypt;
            char[,] matrix = new char[_column, _row];
            int rowMatrix = 0;
            int columnMatrix = 0;

            int symbolIteration = 0;
            bool dimension = true;
            int differenceRow = 0;
            int differenceColumn = 0;
            bool decremnetOrIncrementRow = true;
            bool decremnetOrIncrementColumn = true;
            bool flagRow = false;
            bool flagColumn = false;
            bool flagVertical = true;
            bool flagHorizontal = true;
            for (int iteration = 0; iteration < _row * _column; iteration++)
            {
                matrix[columnMatrix, rowMatrix] = strForEncrypt[symbolIteration];
                symbolIteration++;



                if (((rowMatrix == 0 && !flagVertical) || (rowMatrix == _row - 1 - differenceRow && flagVertical)) && dimension && flagRow)
                {
                    dimension = !dimension;
                    differenceRow++;
                    decremnetOrIncrementRow = !decremnetOrIncrementRow;
                    flagRow = false;
                    flagVertical = !flagVertical;
                }
                else if (((columnMatrix == 0 && !flagHorizontal) || (columnMatrix == columnMatrix - differenceColumn && flagHorizontal)) && !dimension && flagColumn)
                {
                    dimension = !dimension;
                    differenceColumn++;
                    decremnetOrIncrementColumn = !decremnetOrIncrementColumn;
                    flagColumn = false;
                    flagHorizontal = !flagHorizontal;
                }

                if (dimension)
                {
                    rowMatrix = rowMatrix + 1 * (decremnetOrIncrementRow ? 1 : -1);
                    flagRow = true;
                }
                else
                {
                    flagColumn = true;
                    columnMatrix = columnMatrix + 1 * (decremnetOrIncrementColumn ? 1 : -1);
                }
            }
            return str;
        }
    }
}
