using static System.Math;

public class LinEqs{
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

	public static matrix qr_gs_inverse(matrix Q, matrix R){
		matrix B = new matrix(R.size1, R.size1);
		vector e = new vector(B.size1);
		for(int i = 0; i < e.size; i++){ 
			e[i] = 1;
			B[i] = qr_gs_solve(Q,R,e);
			e[i] = 0;
		}
		return B;
	}

	public static vector qr_givens_solve(matrix A, vector b){
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
}
