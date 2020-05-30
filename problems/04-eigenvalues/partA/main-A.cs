using static System.Console;
using static MatrixMethods;
using static System.Math;
using System.IO;

class main{
	static int Main(){
		// Generate matrix A
		int n = 7;
		WriteLine("Creating random 7x7 matrix, A...");
		matrix A = randomMatrix(n, n);
		// Make A symmetric
		restoreUpperTriang(A);
		matrix V = new matrix(n,n);
		vector e = new vector(n); // Vector of eigenvalues
		A.print("A:");
		WriteLine("Calculating eigenvalues...");
		int sweeps = Jacobi.cyclic(A, V, e);
		WriteLine($"Calculation done! Number of sweeps: {sweeps}");
		V.print("Eigenvectors");
		matrix D = new matrix(n,n);
		for(int i = 0; i < n; i++) D[i,i] = e[i];
		D.print("Diagonal composition, D:");
		restoreUpperTriang(A); // Restore A
		matrix test = V.transpose() * A * V;
		test.print("Test that V^T * A * V = D");
		matrix test2 = V * D * V.transpose();
		test2.print("Test that V * D * V^T = A");
		WriteLine("Test first eigenvalue and vector:");
		vector eigvec = A * V[0];
		vector eigval = D[0,0] * V[0];
		eigvec.print("A * V[0] : ");
		eigval.print("D[0,0] * V[0] : ");

		// Calculate numerically Quantum particle in a box
		WriteLine("--- Calculate numerically QM particle in a box");
		// Generate H
		int N = 20; // Number of time units
		double s = 1.0/(N+1);
		matrix H = new matrix(N,N);
		for(int i = 0; i< N-1; i++){
			H[i,i] = -2;
			H[i,i+1] = 1;
			H[i+1, i] = 1;
		}
		H[N-1, N-1] = -2;
		H = H * -1 / s / s;

		// Diagonalize H using jacobi
		V = new matrix(N,N);
		eigvec = new vector(N);
		int hSweeps = Jacobi.cyclic(H, V, eigvec);
		
		// Test that 
		WriteLine("Calculation done: Checking that energies are correct");
		WriteLine("E_n \t Calculation \t Exact");
		for(int k = 0; k < N/3; k++){
			double exact = PI*PI*(k+1)*(k+1);
			double calc = eigvec[k];
			WriteLine($"E_{k} \t  {calc.ToString("N5")} \t {exact.ToString("N5")}");
		}


		// Generate plotting data to plot with
		using (StreamWriter sw = new StreamWriter("data-A.txt")){
			// Plot the first 3 wave funcs 
			// Format: x, 
		}
		
		TextWriter DataWriter = Error;
		//DataWriter.WriteLine("This is in the data stream");
		for(double x = 0; x < 16; x+=1.0/16) DataWriter.WriteLine($"{x} {2*x}");
		
		return 0;
	}

}
