# Makefile of ODE problem
MAKEFILES = partA/Makefile

.PHONY : all test clean

all: $(MAKEFILES)
	(cd partA; make all; mv -f PlotA.svg ..)

test: testing/Makefile
	(cd testing;make all)

clean:
	rm -f *.svg
	(cd partA; make clean)
	(cd testing; make clean)
