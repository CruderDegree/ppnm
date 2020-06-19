MATHDIR = ../../math/
LIBFILES = $(MATHDIR)LinEqs.cs $(MATHDIR)matrix.cs $(MATHDIR)vector.cs $(MATHDIR)Minimization.cs

.PHONY : all clean

all: Answer.txt

Answer.txt: main.exe
	mono $< > $@

main.exe: main.cs lib.dll
	mcs $< -r:lib.dll -out:$@

lib.dll: $(LIBFILES)
	mcs $^ -target:library -out:$@

clean: 
	rm -f *.dll *.txt *.exe
