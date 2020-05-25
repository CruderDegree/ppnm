using System;
public class qr_stuff{
	public static void qr_gs_decomp(matrix A, matrix R){
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
	
	public static vector qr_gs_solve(matrix Q, matrix R, vector b){
		vector x = new vector(R.size1);
		// Make system triangular
		vector c = Q.transpose() * b;

		for (int i = x.size - 1; i >= 0; i--){
			double s = 0;
			int k = i + 1;

			while (k < x.size) {
				s+= R[i, k] * x[k];
				k++;
			}
			x[i] = 1/R[i,i] * (c[i] - s);
		}
		return x;
	}
	
	static matrix qr_gs_inverse(matrix Q, matrix R){
		// Calculate the inverse of A given A = QR
		matrix B = new matrix(R.size1, R.size1);
		vector e = new vector(B.size1);
		for(int i = 0; i < e.size; i++){ 
			e[i] = 1;
			B[i] = qr_gs_solve(Q,R,e);
			e[i] = 0;
		}
		return B;
	}
	
	public static Fit qrFitter(vector x, vector y, vector dy, Func<double,double>[] funcs){
		int n = x.size; int m = funcs.Length;
		// Construct A matrix and b vector (eq. 7)
		matrix A = new matrix(n, m);
		vector b = new vector(n);
		for(int i = 0; i<n; i++){
			b[i] = y[i]/dy[i];
			for(int k = 0; k<m; k++){
				A[i,k] = funcs[k](x[i])/dy[i];
			}
		}
		// Make decomp of A -> QR, with A = Q
		matrix R = new matrix(m,m);
		matrix Q = A;
		qr_gs_decomp(Q, R);
		// Solve upper triangular system R*c = Q^T * b
		vector c = qr_gs_solve(Q, R, b);
		// Calculate Covariance matrix cov = R**-1 * (R**-1)**T
		matrix I = new matrix(m,m);
		for(int i = 0; i < m; i++) I[i,i] = 1;
		matrix RInv = qr_gs_inverse(I, R); 
		matrix cov = RInv * RInv.transpose();
		return new Fit(c, funcs, cov);
	}
}
