using static System.Math;

public class Jacobi{
	public static int cyclic(matrix A, matrix V, vector e){
		// TODO Rewrite method describtion
		/* Calculates the eigenvalues and eigenvectors of A
		 * where v is the vector of eigenvalues and 
		 * V[i] is the eigenvector of the corresponding value v[i]
		 * A must be a real, symmetric matrix and V and empty matrix
		 * */
		int n = A.size1;
		// Turn V into the identity matrix
		for(int i = 0; i<n; i++) V[i,i] = 1;	
		// Convergence criterion
		bool diagChanged = true;
		// d is a vector containing diagonal elements of A
		for(int i = 0; i<n; i++) e[i] = A[i, i];
		// Perform a Jacobi rotation-sweep and check convergence
		int sweeps = 0;
		while(diagChanged){
			sweeps++;
			// Loop over upper triang. elements in A
			for(int p = 0; p < n; p++){
				for(int q = p + 1; q < n; q++){
					double App = e[p]; double Aqq = e[q];
					double Apq = A[p,q];
					// Calculate phi
					double phi = 0.5*Atan2(2*Apq , Aqq - App);
					double c = Cos(phi);
					double s = Sin(phi);

					// Change elements of p,q
					double newApp = c*c*App - 2*c*s*Apq + s*s*Aqq;
					double newAqq = s*s*App + 2*c*s*Apq + c*c*Aqq;
					
					// Check Convergence criterion
					if(newApp != App || newAqq != Aqq){
						// Update diagonal. Zero A[p,q]
						e[p] = newApp; e[q] = newAqq;
						A[p,q] = 0.0;
						// i = 0 ... p-1 --> ip, iq
						for(int i = 0; i < p; i++){
							double Aip = A[i,p]; double Aiq = A[i,q];
							A[i,p] = c*Aip - s*Aiq;
							A[i,q] = s*Aip + c*Aiq;
						}
						// i = p + 1 ... q-1 --> pi, iq
						for(int i = p+1; i < q; i++){
							double Api = A[p,i]; double Aiq = A[i,q];
							A[p,i] = c*Api - s*Aiq;
							A[i,q] = s*Api + c*Aiq;
						}
						// i = q + 1 ... n-1 --> pi, qi
						for(int i = q+1; i < n; i++){
							double Api = A[p,i]; double Aqi = A[q,i];
							A[p,i] = c*Api - s*Aqi;
							A[q,i] = s*Api + c*Aqi;
						}

						// Update V Matrix
						for(int i = 0; i < n; i++){
							double Vip = V[i,p]; double Viq = V[i,q];
							V[i, p] = c*Vip - s*Viq;
							V[i, q] = c*Viq + s*Vip;
						}
					}
					else diagChanged = false;
				}
			}
		}
		// Procedure is done, A --> D, V has been calculated
		return sweeps;
	}
}
