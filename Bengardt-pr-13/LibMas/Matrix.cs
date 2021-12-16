using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMas
{
    public class Matrix
    {
        private readonly Random _random = new Random();
        private int[,] matrix;
        public int RowLength => matrix.GetLength(0);
        public int ColumnLength => matrix.GetLength(1);

        public Matrix(int row, int column)
        {
            matrix = new int[row, column];
        }

        public void Fill(int minvalue = 0, int maxvalue = 10)
        {
            for (int i = 0; i < RowLength; i++)
            {
                for (int j = 0; j < ColumnLength; j++)
                {
                    matrix[i, j] = _random.Next(minvalue, maxvalue);
                }
            }
        }

        public void Export(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(RowLength);
                writer.WriteLine(ColumnLength);

                for (int i = 0; i < RowLength; i++)
                {
                    for (int j = 0; j < ColumnLength; j++)
                    {
                        writer.WriteLine(matrix[i, j]);
                    }
                }
            }
        }

        public void Import(string path)
        {
            using StreamReader reader = new StreamReader(path);
            int rowlength = int.Parse(reader.ReadLine());
            int columnlength = int.Parse(reader.ReadLine());

            for (int i = 0; i < rowlength; i++)
            {
                for (int j = 0; j < columnlength; j++)
                {
                    matrix[i, j] = int.Parse(reader.ReadLine());
                }
            }
        }

        public DataTable ToDataTable()
        {
            var res = new DataTable();
            for (int i = 0; i < RowLength; i++)
            {
                res.Columns.Add("Col" + (i + 1));
            }

            for (int i = 0; i < ColumnLength; i++)
            {
                var row = res.NewRow();

                for (int j = 0; j < ColumnLength; j++)
                {
                    row[j] = matrix[i, j];
                }

                res.Rows.Add(row);
            }

            return res;
        }

        public int this[int indexRow, int indexCoulumn]
        {
            get => matrix[indexRow, indexCoulumn];
            set => matrix[indexRow, indexCoulumn] = value;
        }

        public void Clear()
        {
            Fill(0, 0);
        }
    }
}
