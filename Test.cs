using System;
using System.Collections;
using System.IO;

namespace RLS
{
	class Test
	{ 
		public static ArrayList WriteFile ( String filepath ) 
		{
			ArrayList list = new ArrayList (); 

			// Read from File
			String line;
			try 
			{
				//Pass the file path and file name to the StreamReader constructor
				StreamReader sr = new StreamReader(filepath);

				// Read the first line of text
				line = sr.ReadLine();

				// Continue to read until you reach end of file
				while (line != null) 
				{
					// Write the lie to console window
					double d = Convert.ToDouble( line ) ;
					list.Add( d ) ; 
					//Read the next line
					line = sr.ReadLine();
				}
				// Close the file
				sr.Close();
			}
			catch(Exception e)
			{
				Console.WriteLine("Exception: " + e.Message);
			}
			finally 
			{
				Console.WriteLine("File Succesfully Read.");
			}

			return list; 
		}

		public static void Main (string[] args)
		{
			ArrayList InputSignal = WriteFile ("/home/alfredo/Projects/RLS/RLS/InputSignal.txt"); 
			ArrayList ExpectedOutputSignal = WriteFile ("/home/alfredo/Projects/RLS/RLS/ExpectedOutputSignal.txt");

			Matrix coef = new Matrix ( 2 , 1 , 0 , 0 );

			// Recursive Least Square Filter

			// Parameters
			double lambda = .99f ; // Forgetting factor
			double delta = .01f ; // Initialization random value
			int windowlength = 2 ; // Sliding window length

			RecursiveLeastSquareFiler RLSF = new RecursiveLeastSquareFiler ( lambda, delta , windowlength);

			// Write OutputSignal File
			StreamWriter sw = new StreamWriter("/home/alfredo/Projects/RLS/RLS/OutputSignal.txt");

			double Output = 0.0f; 

			for (int i = 0; i < InputSignal.Count; i++) 
			{
				// Apply RLS Filter to each new iteration
				coef = RLSF.getCoefficients( ( double ) InputSignal[i], 
											 ( double ) ExpectedOutputSignal[i] , 
											  ref Output ); 

				//Write Output Signal
				sw.WriteLine( String.Format("{0:F20}", Output) ) ;

			}
			// Close File
			sw.Close (); 

			// Print Coefficients
			Console.WriteLine ("RLS Filter Coefficients:");
			coef.Print() ;
		
		}
	}
}
