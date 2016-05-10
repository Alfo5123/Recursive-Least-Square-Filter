using System;

namespace RLS
{
	class RecursiveLeastSquareFiler 
	{
		private double lambda ; 
		private double delta ; 
		private int windowLength ;  
		private Matrix Rinv ; 
		private Matrix Coefficients ; 
		private Matrix Input ;

		public RecursiveLeastSquareFiler ( double ilambda, double idelta , int iwindowLength  ) 
		{
			lambda = ilambda; 
			delta = idelta; 
			windowLength = iwindowLength; 

			Coefficients = new Matrix (windowLength, 1 , 0 , 0 ); 
			Input = new Matrix (windowLength, 1 , 0 , 0 ); 
			Rinv = new Matrix (windowLength, windowLength, 1, delta ); 
		}

		public Matrix getCoefficients ( double iNewInput  , double iExpectedOutput , ref double output )
		{
			// Actualize Input Matrix
			for ( int j = windowLength - 1 ; j >= 1 ; j-- )
				Input[ j , 0 ] = Input [ j - 1 , 0 ] ;
			Input[ 0 , 0 ] = iNewInput ;

			// Perform the convolution
			output =  Input.Transpose().Multiply( Coefficients ).GetValue();
			double error = iExpectedOutput - output ; 

			// Compute Kalman Gains
			Matrix K = Rinv.Multiply(Input).Multiply( 1.0f / ( lambda + 
				Input.Transpose().Multiply( Rinv ).Multiply( Input ).GetValue() ) ) ;

			// Update Inverse Matrix
			Rinv = Rinv.Diff( K.Multiply( Input.Transpose().Multiply( Rinv ) ) );
			Rinv = Rinv.Multiply (1.0f / lambda);

			// Update Coefficients by computed RLS filter
			Coefficients = Coefficients.Add( K.Multiply( error )  ) ;

			return Coefficients;
		}
	}
}

