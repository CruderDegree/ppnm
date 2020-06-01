using static System.Console;
using static MatrixMethods;

class Testing{
	static void Main(){
		int n = 6;
		WriteLine($"Generate symmetric {n}x{n} matrix A");
		matrix A = randomMatrix(n,n);
		restoreUpperTriang(A);
		A.print("A: ");

		matrix V = new matrix(n,n);
		vector e = new vector(n);
		
		int sweeps;

		// Cyclic
		sweeps = Jacobi.cyclic(A, V, e);
		WriteLine($"Cyclic Jacobi done in {sweeps} sweeps");
		e.print("Cyclic eigenvalues: ");
		
		// Lowest k
		restoreUpperTriang(A);
		V = new matrix(n,n);
		e = new vector(n);
		int k = n/3;
		vector v = Jacobi.lowestK(A, V, e, k);
		v.print($"Lowest {k} values:");
		A.print($"Test that {k} rows are cleared");

		// Highest k
		restoreUpperTriang(A);
		V = new matrix(n,n);
		e = new vector(n);
		k = n/3;
		v = Jacobi.highestK(A, V, e, k);
		v.print($"Highest {k} values:");
		A.print($"Test that {k} rows are cleared");

		restoreUpperTriang(A);
		V = new matrix(n,n);
		e = new vector(n);
		int rots = Jacobi.classic(A, V, e);
		e.print($"Classical done! rotations: {rots}");
		A.print($"Test that all rows are cleared");
	}
}
