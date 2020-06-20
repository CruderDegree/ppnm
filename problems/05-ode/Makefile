# Makefile of ODE problem
MAKEFILES = partA/Makefile partB/Makefile partC/Makefile

.PHONY : all test clean

all: $(MAKEFILES)
	(cd partA; make all; mv -f PlotA.svg ..)
	(cd partB; make all; mv -f PlotB.svg ..)
	(cd partC; make all; mv -f PlotC.svg ..)

test: testing/Makefile
	(cd testing;make all)

clean:
	rm -f *.svg
	(cd partA; make clean)
	(cd partB; make clean)
	(cd partC; make clean)
	(cd testing; make clean)
