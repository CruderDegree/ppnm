using static System.Console;
using System;

class main{
	static int Main(){
		// Generate random matrix A
		WriteLine("Part 1: QR-GS decomposition");
		WriteLine("Generating random n*m matrix A...");
		Random random = new System.Random();
		int r = random.Next(8, 16);
		int c = random.Next(r/2, r);
		matrix A = new matrix(r,c);
		for(int i = 0; i< r; i++){
			for(int j = 0; j < c; j++){ 
				A[i, j] = random.Next(-32, 32);
			}
		}
		// QR Decomp of A
		matrix R = new matrix(c, c);
		A.print("A:");
		R.print("R:");
		WriteLine("Make decomposition");
		matrix Q = A.copy();
		qr_gs_decomp(Q, R);
		Q.print("Q: ");
		R.print("R: ");
		(Q.transpose() * Q).print("Q^T * Q: ");
		matrix temp = Q*R;
		temp.print("Q*R = A :");
		
		
		// Linear Eq Solver
		WriteLine("Part 2: QR-GS linear eq. solver");
		WriteLine("Generating new random m*m matrix A...");
		int m = random.Next(5, 16);
		A = new matrix(m,m);
		for(int i = 0; i< m; i++){
			for(int j = 0; j < m; j++){ 
				A[i, j] = random.Next(-32, 32);
			}
		}
		A.print("A:");
		Q = A.copy();
		R = new matrix(m,m);
		WriteLine("Generating random vector b...");
		vector b = new vector(m);
		for (int i = 0; i < m; i++) b[i] = random.Next(-64, 64);
		b.print("vector b: ");
		// Solve QRx = b
		WriteLine("Decomposing A into QR");
		qr_gs_decomp(Q, R);
		WriteLine("Solving Ax = b ...");
		vector x = qr_gs_solve(Q, R, b);
		x.print("x:");
		vector b_test = A * x;
		b_test.print("Test that Ax = b");
		
		return 0;
	}

	static void qr_gs_decomp(matrix A, matrix R){
		int m = A.size2;
		for(int i = 0; i<m; i++){
			vector a_i = A[i];
			R[i,i] = a_i.norm();
			vector q = a_i/R[i,i];
			A[i] = q;		
			for(int j = i + 1; j < m; j++){
				vector a_j = A[j];
				R[i,j] = q.dot(a_j);
				A[j] = a_j - q*R[i,j];
			}
		}
	}
	
	static vector qr_gs_solve(matrix Q, matrix R, vector b){
		vector x = new vector(R.size1);
		// Make system triangular
		vector c = Q.transpose() * b;
		matrix U = R.copy();

		for (int i = x.size - 1; i >= 0; i--){
			double s = 0;
			int k = i + 1;

			while (k < x.size) {
				s+= U[i, k] * x[k];
				k++;
			}
			x[i] = 1/U[i,i] * (c[i] - s);
		}
		return x;
	}
}	
