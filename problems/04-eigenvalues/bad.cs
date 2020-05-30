using System;

public class randomGen{
	public static matrix randomMatrix(int n, int m){
		// Create an n*m matrix filled with random integers
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
		Random random = new System.Random();
		vector v = new vector(n);
		for (int i = 0; i < n; i++) v[i] = random.Next(-64, 64);
		return v;
	}
}
