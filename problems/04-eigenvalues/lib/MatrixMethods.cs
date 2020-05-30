using System;

public class MatrixMethods{
	/* --- MatrixMethods Class ---
	 * This class contains a number of methods useful when working with
	 * the matrix and vector classes of the PPNM course.
	 */

	public static void restoreUpperTriang(matrix L){
		// L is a square, lower triangular matrix
		// Mirrors the lower triangular part of L 
		// into the upper part, in place
		if(L.size1 != L.size2) throw new ArgumentException($"ERROR: Matrix is not square, size 1: {L.size1}, size2: {L.size2}"); 
		int n = L.size1;
		for(int i = 0; i < n; i++){
			for(int j = i+1; j < n; j++){
				L[i,j] = L[j,i];
			}
		}
	}
	
	public static matrix randomMatrix(int n, int m){
		// Create an n*m matrix filled with random integers
		// between -32 and 32
		Random random = new System.Random();
		matrix A = new matrix(n,m);
		for(int i = 0; i< n; i++){
			for(int j = 0; j < m; j++){ 
				A[i, j] = random.Next(-32, 32);
			}
		}
		return A;
	}
	public static vector randomVector(int n){
		// Create a vector of size n with random integers
		// between -64 and 64
		Random random = new System.Random();
		vector v = new vector(n);
		for (int i = 0; i < n; i++) v[i] = random.Next(-64, 64);
		return v;
	}
}
