# Makefile of exam problem # 21
LIBFILES = ../math/Jacobi.cs ../math/matrix.cs ../math/vector.cs ../math/MatrixMethods.cs

.PHONY : all clean

all : Answer.txt 

Answer.txt : main.exe
	mono $< > $@

main.exe : main.cs lib.dll
	mcs $< -r:lib.dll

lib.dll : $(LIBFILES)
	mcs $^ -target:library -out:$@

clean : 
	rm -f *.dll *.exe *.txt


