# Makefile of Eigenvalues problem
MAKEFILES = partA/Makefile partB/Makefile partC/Makefile 

all: $(MAKEFILES)
	(cd partA; make all; mv -f Answer-A.txt Plot-A.svg ..)
	(cd partB; make all; mv -f Plot-B.svg ..)
	(cd partC; make all; mv -f Plot-C.svg ..)

clean:
	rm -f *.svg *.txt
	(cd partA; make clean)
	(cd partB; make clean)
	(cd partC; make clean)
