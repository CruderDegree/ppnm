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
		int N = 32; // Number of numerical steps
		matrix H = new matrix(N,N);
		for(int i = 0; i< N-1; i++){
			H[i,i] = -2;
			H[i,i+1] = 1;
			H[i+1, i] = 1;
		}
		H[N-1, N-1] = -2;
		H = -(N+1) * (N+1) * H;

		// Diagonalize H using jacobi
		matrix boxV = new matrix(N,N);
		vector boxE = new vector(N);
		int hSweeps = Jacobi.cyclic(H, boxV, boxE);
		
		// Test that 
		WriteLine("Calculation done: Checking that energies are correct");
		WriteLine("E_n \t Calculation \t Exact");
		for(int k = 0; k < N/3; k++){
			double exact = PI*PI*(k+1)*(k+1);
			double calc = boxE[k];
			WriteLine($"E_{k} \t  {calc.ToString("N5")} \t {exact.ToString("N5")}");
		}


		// Generate plotting data to plot with
		using (StreamWriter sw = new StreamWriter("data-A.txt")){
			// Plot the first 3 wave funcs 
			// Format: x \t psi0 \t psi_n ...
			sw.WriteLine($"0 \t 0 \t 0 \t 0 \t 0 \t 0");
			for(int i = 0; i < N; i++){
				sw.Write($"{(i + 1)*1.0/N} ");
				for(int k = 0; k < 5; k++) sw.Write($"\t {boxV[i,k]} ");
				sw.Write("\n");
			}
		}
		return 0;
	}

}
