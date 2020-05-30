# Makefile of Eigenvalues problem
MAKEFILES = partA/Makefile

all: $(MAKEFILES)
	(cd partA; make all; mv -f Answer-A.txt Plot-A.svg ..)

clean:
	rm -f *.svg *.txt
	(cd partA; make clean)
