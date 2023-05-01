/*
 * Vytvořeno aplikací SharpDevelop.
 * Uživatel: Amon
 * Datum: 02.12.2021
 * Čas: 18:14
 * 
 * Tento template můžete změnit pomocí Nástroje | Možnosti | Psaní kódu | Upravit standardní hlavičky souborů.
 */
using System;
/*
 * počítat skóre hráčům = 2 proměnné pro hráče
 */

namespace AmonPexeso
{
	class Program
	{	
		public static void Main(string[] args) {				
			string[] poleKaret          = {"A","B","C","D", "A","B","C","D","E","E","F","F","G","G","H","H"};			
			string[] poleKaretAktualni  = new string[16];	
			
			ZamichejKarty(poleKaret);
			
			Array.ForEach(poleKaret, Console.WriteLine);
			
 			PripravKarty(poleKaretAktualni);
 				 			
			Hra(poleKaret, poleKaretAktualni);
		}		
		
		public static void ZamichejKarty(string[] poleKaret) {
			var rng = new Random();
			int n = poleKaret.Length;
	        while (n > 1) {
	            int k = rng.Next(n--);        
	            string docasny = poleKaret[n];
	            poleKaret[n] = poleKaret[k];
	            poleKaret[k] = docasny;
	        }		
		}
		
		public static void PripravKarty(string[] poleKaret) {
			for (int i = 0; i < poleKaret.Length; i++) {
				poleKaret[i] = "#";
			}	
		}
		
		public static void VylozKarty(string[] poleKaret) {			
			Console.WriteLine();
			for (int i = 0; i < poleKaret.Length; i++) {
				Console.Write(poleKaret[i]);
				if ((i+1)%4==0) {
					Console.WriteLine();
				}	
			}
			Console.WriteLine();			
		}
		
		public static void VypisUvod(int[] hraci, int hrac, int tah) {
			NastavBarvu(0);					
			Console.WriteLine("SKÓRE");
			Console.WriteLine("Hráč č.1:{0}", hraci[0]);
			NastavBarvu(1);	
			Console.WriteLine("Hráč č.2:{0}", hraci[1]);
			Console.WriteLine();
			
			NastavBarvu(hrac);	
			Console.WriteLine("Právě hraje Hráč č. {0}, TAH {1}", hrac+1, tah+1);
			Console.WriteLine();
		}
		
		public static void NastavBarvu(int hrac) {
			if(hrac == 1) {					
				Console.ForegroundColor = ConsoleColor.Red;
			} else {
				Console.ForegroundColor = ConsoleColor.White;				
			}
		}
		
		public static int PrepniHrace(int hrac) {
			if(hrac == 1) {
				return 0;        					
			} else {
				return 1;        					
			}			
		}
		
		public static Boolean NeniOdkryta(string[] poleKaretAktualni, int pozice) {
			if(poleKaretAktualni[pozice] == "#") {
				return true;
			}
			return false;
		}
		
		public static int ZadejKartu(string[] poleKaretAktualni) {
			int cislo=0;
			
			do {
				do {
					var precti = Console.ReadLine();
					while(!int.TryParse(precti, out cislo)) {
						Console.WriteLine("Zadej cislo!");	
						precti = Console.ReadLine();
					}
					if ((cislo < 1) || (cislo > 16)) {
						Console.WriteLine("Zadej cislo v rozsahu od 1 do 16!");					
					}
				} while((cislo < 1) || (cislo > 16));	
				if (!NeniOdkryta(poleKaretAktualni, cislo-1)) {
					Console.WriteLine("Zadej cislo karty, ktera jeste nebyla odkryta");					
				}				
			} while(!NeniOdkryta(poleKaretAktualni, cislo-1));
		

			return cislo;
		}
		
		public static Boolean PorovnejPole(string[] pole1, string[] pole2, int size) {
			int i;
			
			for (i=0; i<size; i++) {
				if(pole1[i] != pole2[i]) return false;
			}
			return true;
		}
		
		public static void VypisPoTahu(int coVypsat) {
			if (coVypsat == 1) {
					Console.WriteLine("Skvele mas o bod navic, hraješ znovu.");	
			} 
		}
		
		public static void ZaverecnaZprava(int[] hraci) {
			
			Console.WriteLine(); 
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("Konec hry.");
			
			if(hraci[0] == hraci[1]) {
				Console.WriteLine("Remíza!");
			} else if(hraci[0] > hraci[1]) {
				Console.WriteLine("Vyhrál hráč č.1!");
			} else if(hraci[0] < hraci[1]) {
				Console.WriteLine("Vyhrál hráč č.2!");
			}

			Console.WriteLine("Stiskni klávesu pro konec hry."); 			
		}
		
		public static void Hra(string[] poleKaret, string[] poleKaretAktualni) {
			int vstup;			
			int tah        = 0;             // každý část hry se skládá ze dvou tahů tah 0 a tah 1 výsledky se ukládají do pole volby
			string[] karty = {"",""};
			int[] indexy   = new int[2];
			
			int hrac       = 0;             // hraci jsou v poli index 0 jeden hráč, index 1 druhý hráč
			int[] hraci    = {0, 0};
			
			int coVypsatPoTahu = 0;
			
			Console.Clear();
			
			VypisUvod(hraci, hrac, tah);
			
			VylozKarty(poleKaretAktualni);
			//VylozKarty(poleKaret);
			
	        while (true) {
				
				vstup = ZadejKartu(poleKaretAktualni) - 1;
        		poleKaretAktualni[vstup] = poleKaret[vstup];        		
        		karty[tah]               = poleKaretAktualni[vstup]; 
        		indexy[tah]              = vstup;
        		       		
        		++tah;	
				
				if (tah > 1) {
        			if (karty[0] == karty[1]) {
        				++hraci[hrac];
        				if(PorovnejPole(poleKaret, poleKaretAktualni, poleKaret.Length)) break;
        				// pokud se rovnají dvě volby tak se přidá skóre hrajícímu hráči a hráč pokračuje
        				coVypsatPoTahu = 1;
						tah = 0;          				
        			} else {	
						coVypsatPoTahu = 2;
						tah = 1;   
        			}
        		} 	
        		
        		Console.Clear();
				VypisUvod(hraci, hrac, tah);
				VylozKarty(poleKaretAktualni);
				//VylozKarty(poleKaret);				
				
				if (coVypsatPoTahu == 1) {
					Console.WriteLine("Skvele mas o bod navic, hraješ znovu.");	
				} else if (coVypsatPoTahu == 2) {
					poleKaretAktualni[indexy[0]] = "#";
					poleKaretAktualni[indexy[1]] = "#";
    				hrac = PrepniHrace(hrac);
    				tah = 0;  
    				// pokud se dvě volby nerovnají tak se skóre nemění, ale změní se hráč
    				Console.WriteLine("Bohuzel jsi neuhadl, stiskni klávesu aby hrál protihráč."); 	
    				Console.ReadLine(); 
    				
					Console.Clear();
					VypisUvod(hraci, hrac, tah);
					VylozKarty(poleKaretAktualni);					
				}

				coVypsatPoTahu = 0;				
	        }
			Console.Clear();
			VypisUvod(hraci, hrac, tah);
			VylozKarty(poleKaretAktualni);	
			//VylozKarty(poleKaret);				
						
			ZaverecnaZprava(hraci);
			Console.ReadLine();
		}
	}
}