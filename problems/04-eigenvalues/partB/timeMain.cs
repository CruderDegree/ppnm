using System.Diagnostics;
using static System.Console;
using static MatrixMethods;
using System;

class main{
	static void Main(string[] args){
		/* Compare the time it takes to diagonalize a matrix 
		 * of size n by different methods:
		 * Cyclic jacobi, value-by-value and v-b-v
		 * for both highest and lowest eig val.
		 */ 
		Stopwatch sw = new Stopwatch();
		foreach (string arg in args){
			int n = Int32.Parse(arg);
			
			// Generate symmetric nxn matrix A
			matrix A = randomMatrix(n,n);
			restoreUpperTriang(A);

			// Cyclic jacobi
			matrix V = new matrix(n,n);
			vector e = new vector(n);
			sw.Start();
			int sweeps = Jacobi.cyclic(A, V, e);
			sw.Stop();
			double cyclic = sw.Elapsed.Ticks/10000.0;
			sw.Reset();
		
			// Value-by-value full
			V = new matrix(n,n);
			e = new vector(n);
			sw.Start();
			vector eigVals = Jacobi.lowestK(A, V, e, n);
			sw.Stop();
			double VBVfull = sw.Elapsed.Ticks/10000.0;
			sw.Reset();

			// Value-by-value 1
			V = new matrix(n,n);
			e = new vector(n);
			sw.Start();
			eigVals = Jacobi.lowestK(A, V, e, 1);
			sw.Stop();
			double VBVlow = sw.Elapsed.Ticks/10000.0;
			sw.Reset();

			// Value-by-value 1, highest first
			V = new matrix(n,n);
			e = new vector(n);
			sw.Start();
			eigVals = Jacobi.highestK(A, V, e, 1);
			sw.Stop();
			double VBVhigh = sw.Elapsed.Ticks/10000.0;
			sw.Reset();

			// size of matrix and time in ms
			WriteLine($"{n} {cyclic} {VBVfull} {VBVlow} {VBVhigh}");
		}
	}
}
