using static System.Console;
using static MatrixMethods;
using static Jacobi;
using System;
using System.Diagnostics;

class main{
	static void Main(){
		int[] ns = new int[] 
		{ 4, 5, 6, 10, 15, 20, 25, 40, 50, 60, 75, 80, 90, 100, 125,
		150, 175, 200, 225, 250, 275, 300};
		Stopwatch sw = new Stopwatch();
		foreach(int n in ns){
			// Generate symmetric matrix A
			matrix A = randomMatrix(n,n);
			
			// Cyclic algorithm
			restoreUpperTriang(A);
			matrix V = new matrix(n,n);
			vector e = new vector(n);

			sw.Start();
			int sweeps = cyclic(A, V, e);
			sw.Stop();
			double cycT = sw.Elapsed.Ticks / 10000.0;
			int cycRot = sweeps * n*(n+1) / 2;
			sw.Reset();
			
			// Classical algorithm
			restoreUpperTriang(A);
			V = new matrix(n,n);
			e = new vector(n);

			sw.Start();
			int claRot = classic(A, V, e);
			sw.Stop();
			double claT = sw.Elapsed.Ticks / 10000.0;
			sw.Reset();

			WriteLine($"{n} {cycRot} {cycT} {claRot} {claT}");
		}
	}
}
