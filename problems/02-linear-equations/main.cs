using static System.Console;
using static System.Math;
using System;

class main{
	static int Main(){
		WriteLine("Part A: QR-GS decomposition");
		WriteLine("Generating random n*m matrix A...");
		Random random = new System.Random();
		// QR Decomp of A
		int r = random.Next(5, 8);
		int c = random.Next(r/2, r);
		matrix A = randomMatrix(r,c);
		matrix R = new matrix(c, c);
		A.print("A:");
		WriteLine("Make QR-decomposition");
		matrix Q = A.copy();
		qr_gs_decomp(Q, R);
		Q.print("Q: ");
		R.print("R: ");
		(Q.transpose() * Q).print("Test Q^T * Q = I :");
		matrix temp = Q*R;
		temp.print("Test Q*R = A :");
		
		
		// Linear Eq Solver
		WriteLine("QR-GS linear eq. solver");
		WriteLine("Generating new random m*m matrix A...");
		int m = random.Next(5, 9);
		A = randomMatrix(m,m);
		A.print("A:");
		Q = A.copy();
		R = new matrix(m,m);
		WriteLine("Generating random vector b...");
		vector b = randomVector(m);
		b.print("vector b: ");
		// Solve QRx = b
		WriteLine("Decomposing A into QR");
		qr_gs_decomp(Q, R);
		WriteLine("Solving Ax = b ...");
		vector x = qr_gs_solve(Q, R, b);
		x.print("x:");
		vector b_test = A * x;
		b_test.print("Test that Ax = b");
		
		WriteLine("");
		WriteLine("Part B: Inverting Matrices");
		WriteLine("");
		WriteLine("Re-using random square matrices from Part 2...");
		WriteLine("Calculating A**-1 = B...");
		matrix B = qr_gs_inverse(Q, R);
		B.print("B: ");
		matrix I = A*B;
		I.print("Test A*B = I: ");
		
		WriteLine("Part C: Use Givens rotations to solve Ax = b");
		WriteLine("Generate new matrix A");
		int n = random.Next(5, 8); m = random.Next(n, n+4);
		A = randomMatrix(n,m);
		A.print("A: ");
		WriteLine("and new vector b");
		b = randomVector(n);
		b.print("b: ");
		WriteLine("Solving Ax = b using Givens rotations...");
		matrix A_copy = A.copy();
		x = qr_givens_solve(A, b);
		x.print("Solution x:");
		b_test = A_copy*x;
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

	static matrix qr_gs_inverse(matrix Q, matrix R){
		matrix B = new matrix(R.size1, R.size1);
		vector e = new vector(B.size1);
		for(int i = 0; i < e.size; i++){ 
			e[i] = 1;
			B[i] = qr_gs_solve(Q,R,e);
			e[i] = 0;
		}
		return B;
	}

	static vector qr_givens_solve(matrix A, vector b){
		int n = A.size1; int m = A.size2;
		vector x = new vector(m);
		// Make A -> R, upper triangular matrix
		for(int p = 0; p < m; p++){
			for(int q = n - 1; q > p; q--){
				// Calculate theta_qp
				double theta = Atan(1.0*A[q,p]/A[p, p]);
				// G*A
				for(int i = p; i < A.size2; i++){
					double xp = A[p,i];
					double xq = A[q,i];
					A[p,i] = xp*Cos(theta) + xq*Sin(theta);
					A[q,i] = -xp*Sin(theta) + xq*Cos(theta);
				}
				// G*b, instead of storing in R we do this immediatly
				double bp = b[p];
				double bq = b[q];
				b[p] = bp*Cos(theta) + bq*Sin(theta);
				b[q] = -bp*Sin(theta) + bq*Cos(theta);
			}
		}
		// Solve Rx = Gb using back-substituion
		for(int i = n-1; i>=0; i--){
			double sum = 0;
			for(int k = i + 1; k < n; k++) sum += A[i,k]*x[k];
			x[i] = (b[i] - sum)/A[i,i];
		}
		return x;
	}

	static matrix randomMatrix(int n, int m){
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
	static vector randomVector(int n){
		// Create a vector of size n with random integers
		Random random = new System.Random();
		vector v = new vector(n);
		for (int i = 0; i < n; i++) v[i] = random.Next(-64, 64);
		return v;
	}
}	
