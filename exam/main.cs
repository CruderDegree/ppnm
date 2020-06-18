using static Jacobi;
using static MatrixMethods;
using static System.Console;

class main{
	static void Main(){
		WriteLine("Exam problem 21: \nTwo-sided Jacobi algorithm for Singular Value Decomposition (SVD) \n");;
		int n = 7;
		WriteLine($"Generating Random {n}x{n} matrix");
		matrix A = randomMatrix(n,n), U = new matrix(n,n), V = new matrix(n,n);
		matrix D = A.copy();
		A.print("Matrix A generated:");
		WriteLine("Making composition...");
		int sweeps = twoSidedJacobiSVD(D,U,V);
		WriteLine($"Procedure done!\nNo. of sweeps: {sweeps}.\nNow A --> U*D*V^T, where");
		D.print("D: ");
		U.print("U: ");
		V.print("V: ");
		matrix R = (U*D)*V.transpose();
		R.print("Test that UDV^T = A");
		A.print("A, for reference");
		matrix UUt = U*U.transpose(), VVt = V*V.transpose();
		UUt.print("Test U*U^T = 1");
		VVt.print("Test V*V^T = 1");
	}
}
