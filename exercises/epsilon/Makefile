.PHONEY:all
all: out.txt
	cat $<

out.txt : epsilon.exe 
	mono $< > $@

epsilon.exe : epsilon.cs
	mcs $<

.PHONEY:clean
clean: 
	rm out.txt epsilon.exe
