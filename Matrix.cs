using System;

namespace RLS
{
	class Matrix 
	{
		public int rows ; 
		public int cols ; 
		public double[,] matrix ; 

		public Matrix ( int iRows , int iCols , int ind , double iDelta )
		{
			rows = iRows; 
			cols = iCols; 
			matrix = new double[ rows, cols ]; 

			if (ind == 0)
			{
				for (int i = 0; i < iRows; i++)
					for (int j = 0; j < iCols; j++)
						matrix [i, j] = 0; 
			} 

			if (ind == 1) 
			{
				for (int i = 0; i < Math.Min (iRows, iCols); i++)
					matrix [i, i] = iDelta ; 
			}
		}

		public double this[ int iRows , int iCols ] 
		{
			get{ return matrix [iRows, iCols]; }
			set{ matrix [iRows, iCols] = value; }
		}

		public Matrix Transpose ( )
		{
			Matrix outMatrix = new Matrix ( cols, rows , 0 , 0 ); 
			for (int i = 0; i < rows; i++)
				for (int j = 0; j < cols; j++)
					outMatrix [j, i] = matrix [i, j]; 
			return outMatrix ; 
		}

		public Matrix Multiply ( Matrix m )
		{
			if ( cols != m.rows )
				throw new Exception ("Wrong Dimensions"); 

			Matrix outMatrix = new Matrix( rows, m.cols , 0 , 0);
			for (int i = 0; i < outMatrix.rows; i++)
				for (int j = 0; j < outMatrix.cols; j++)
					for (int k = 0; k < m.rows; k++)
						outMatrix [i, j] +=  matrix[i, k] * m[k, j];
			return outMatrix ; 
		}

		public Matrix Multiply ( double f )
		{
			Matrix outMatrix = this ; 
			for (int i = 0; i < rows; i++)
				for (int j = 0; j < cols; j++)
					outMatrix [i, j] *= f ; 
			return outMatrix;
		}

		public Matrix Add (  Matrix m ) 
		{
			Matrix outMatrix = this ; 
			for (int i = 0; i < rows; i++)
				for (int j = 0; j < cols; j++)
					outMatrix [i, j] += m[i, j]; 
			return outMatrix; 
		}

		public Matrix Diff (  Matrix m ) 
		{
			Matrix outMatrix = this ; 
			for (int i = 0; i < rows; i++)
				for (int j = 0; j < cols; j++)
					outMatrix [i, j] -= m[i, j]; 
			return outMatrix; 
		}

		public double GetValue ( )
		{
			if ( cols != 1 || rows != 1 )
				throw new Exception ("Wrong Dimensions"); 
			return this.matrix[0,0];
		}

		public void Print ()
		{
			for (int i = 0; i < rows; i++) 
				Console.Write(string.Format("{0} ", matrix[i, 0]));
			Console.WriteLine ();
		}
	}
}
