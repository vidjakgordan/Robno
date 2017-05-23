using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Robno.Models
{
    public class RobnoContextInitializer : DropCreateDatabaseAlways<RobnoContext>
    {
        protected override void Seed(RobnoContext context)
        {
            //JEDINICAMJERE
            var jedinicamjere = new List<JedinicaMjere>(){
                new JedinicaMjere(){Naziv="Kilogram", Kratica="KG"},
                new JedinicaMjere(){Naziv="Komad", Kratica="KOM"},
                new JedinicaMjere(){Naziv="Litra", Kratica="L"}
            };
            jedinicamjere.ForEach(p => context.JedinicaMjeres.Add(p));
            context.SaveChanges();

            //ARTIKALKLASA
            var artikalklasa = new List<ArtikalKlasa>(){
                new ArtikalKlasa(){Naziv="Rasuti proizvod", Kratica="RASUT"},
                new ArtikalKlasa(){Naziv="Ostali proizvod", Kratica="OSTALI"},
                new ArtikalKlasa(){Naziv="Ekskluzivni proizvod", Kratica="EKSKLUZIVNI"}
            };
            artikalklasa.ForEach(p => context.ArtikalKlasas.Add(p));
            context.SaveChanges();

            //TARIFA
            var tarifa = new List<Tarifa>(){
                new Tarifa(){Opis="PDV 25%", Stopa=25},
                new Tarifa(){Opis="PDV 13%", Stopa=13},
                new Tarifa(){Opis="PDV 0%", Stopa=0} 
            };
            tarifa.ForEach(p => context.Tarifas.Add(p));
            context.SaveChanges();

            //VALUTA
            var valuta = new List<Valuta>(){
                new Valuta(){Naziv="Australski dolar", Kratica="AUD"},
                new Valuta(){Naziv="Euro", Kratica="EUR"},
                new Valuta(){Naziv="Americki dolar", Kratica="USD"},
                new Valuta(){Naziv="Kuna", Kratica="KN"}
            };
            valuta.ForEach(p => context.Valutas.Add(p));
            context.SaveChanges();

            //NACINPLACANJA
            var nacinplacanja = new List<NacinPlacanja>(){
                new NacinPlacanja(){Naziv="Gotovina", Kratica="G"},
                new NacinPlacanja(){Naziv="Transakcijski racun", Kratica="T"},
                new NacinPlacanja(){Naziv="Kartica", Kratica="K"}
            };
            nacinplacanja.ForEach(p => context.NacinPlacanjas.Add(p));
            context.SaveChanges();

            //POSLOVNIPARTNER
            var poslovnipartner = new List<PoslovniPartner>(){
                new PoslovniPartner(){
                    Naziv="AGROKOR", Adresa="xxx", Mjesto="Zagreb", OIB="123456", Tel="123456", Fax="123456"
                },
                new PoslovniPartner(){
                    Naziv="FRANCK", Adresa="xxx", Mjesto="Zagreb", OIB="222222", Tel="222222", Fax="222222"
                }
            };
            poslovnipartner.ForEach(p => context.PoslovniPartners.Add(p));
            context.SaveChanges();

            //PRIMKA
            var primka = new List<Primka>(){
                new Primka(){
                    DatumUnosa=DateTime.Now, UkupniIznos=500, PoslovniPartner=context.PoslovniPartners.FirstOrDefault(x=> x.Naziv=="AGROKOR"),
                    Valuta=context.Valutas.FirstOrDefault(x=>x.Naziv=="Kuna")
                },
                new Primka(){
                    DatumUnosa=DateTime.Now, UkupniIznos=1000, PoslovniPartner=context.PoslovniPartners.FirstOrDefault(x=> x.Naziv=="FRANCK"),
                    Valuta=context.Valutas.FirstOrDefault(x=>x.Naziv=="Euro"), ValutaTecaj=7.34
                }
            };
            primka.ForEach(p => context.Primkas.Add(p));
            context.SaveChanges();

            //ARTIKAL
            var artikal = new List<Artikal>(){
                new Artikal(){
                    Naziv="Hrana za pse", Opis="tralal", BarCode="123456", Tezina=2, NabavnaCijena=90, ProdajnaCijena=250, Kolicina=17,
                    JedinicaMjere=context.JedinicaMjeres.FirstOrDefault(x=>x.Kratica=="KG"), 
                    ArtikalKlasa=context.ArtikalKlasas.FirstOrDefault(x=>x.Kratica=="OSTALI"), 
                    Tarifa = context.Tarifas.FirstOrDefault(x=>x.Stopa==25)
                },
                new Artikal(){
                    Naziv="Hrana za macke", Opis="tralal", BarCode="555888", Tezina=3, NabavnaCijena=10, ProdajnaCijena=100, Kolicina=3,
                    JedinicaMjere=context.JedinicaMjeres.FirstOrDefault(x=>x.Kratica=="KG"), 
                    ArtikalKlasa=context.ArtikalKlasas.FirstOrDefault(x=>x.Kratica=="OSTALI"), 
                    Tarifa = context.Tarifas.FirstOrDefault(x=>x.Stopa==25)
                },
                new Artikal(){
                    Naziv="Mlijeko kravica 12%", Opis="mlijeko za ugostiteljstvo", BarCode="789654", Tezina=0, NabavnaCijena=3, ProdajnaCijena=10, Kolicina=25,
                    JedinicaMjere=context.JedinicaMjeres.FirstOrDefault(x=>x.Kratica=="KOM"), 
                    ArtikalKlasa=context.ArtikalKlasas.FirstOrDefault(x=>x.Kratica=="OSTALI"), 
                    Tarifa = context.Tarifas.FirstOrDefault(x=>x.Stopa==13)
                }
            };
            artikal.ForEach(p => context.Artikals.Add(p));
            context.SaveChanges();

            //STAVKAPRIMKE
            var stavkaprimke = new List<StavkaPrimke>(){
                new StavkaPrimke(){
                    RedniBrojStavke=1, Kolicina=17, NabavnaCijena=100, Rabat=10,
                    Primka=context.Primkas.FirstOrDefault(x=>x.PrimkaID==1),
                    Artikal=context.Artikals.FirstOrDefault(x=>x.Naziv=="Hrana za pse")
                },
                new StavkaPrimke(){
                    RedniBrojStavke=1, Kolicina=3, NabavnaCijena=10, Rabat=0,
                    Primka=context.Primkas.FirstOrDefault(x=>x.PrimkaID==2),
                    Artikal=context.Artikals.FirstOrDefault(x=>x.Naziv=="Hrana za macke")
                },
                new StavkaPrimke(){
                    RedniBrojStavke=2, Kolicina=25, NabavnaCijena=10, Rabat=70,
                    Primka=context.Primkas.FirstOrDefault(x=>x.PrimkaID==2),
                    Artikal=context.Artikals.FirstOrDefault(x=>x.Naziv=="Mlijeko kravica 12%")
                }
            };
            stavkaprimke.ForEach(p => context.StavkaPrimkes.Add(p));
            context.SaveChanges();

            //RACUN
            var racun = new List<Racun>(){
                new Racun(){
                    DatumIzdavanja=DateTime.Now, ZKI="222", JIR="555", UkupniIznos=250,
                    PoslovniPartner=context.PoslovniPartners.FirstOrDefault(x=> x.Naziv=="FRANCK"),
                    NacinPlacanja=context.NacinPlacanjas.FirstOrDefault(x=>x.Kratica=="G")
                },
                new Racun(){
                    DatumIzdavanja=DateTime.Now, ZKI="333", JIR="444", UkupniIznos=100,
                    NacinPlacanja=context.NacinPlacanjas.FirstOrDefault(x=>x.Kratica=="K")
                }
            };
            racun.ForEach(p => context.Racuns.Add(p));
            context.SaveChanges();

            //STAVKARACUNA
            var stavkaracuna = new List<StavkaRacuna>(){
                new StavkaRacuna(){
                    RedniBrojStavke=1, Kolicina=3, NabavnaCijena=10, ProdajnaCijena=20, Popust=0,
                    Tarifa=context.Tarifas.FirstOrDefault(x=>x.Stopa==25),
                    Artikal=context.Artikals.FirstOrDefault(x=>x.Naziv=="Mlijeko kravica 12%"),
                    Racun=context.Racuns.FirstOrDefault(x=>x.RacunID==1)
                },
                new StavkaRacuna(){
                    RedniBrojStavke=2, Kolicina=3, NabavnaCijena=20, ProdajnaCijena=30, Popust=10,
                    Tarifa=context.Tarifas.FirstOrDefault(x=>x.Stopa==25),
                    Artikal=context.Artikals.FirstOrDefault(x=>x.Naziv=="Hrana za macke"),
                    Racun=context.Racuns.FirstOrDefault(x=>x.RacunID==1)
                },
                new StavkaRacuna(){
                    RedniBrojStavke=1, Kolicina=5, NabavnaCijena=100, ProdajnaCijena=250, Popust=20,
                    Tarifa=context.Tarifas.FirstOrDefault(x=>x.Stopa==13),
                    Artikal=context.Artikals.FirstOrDefault(x=>x.Naziv=="Hrana za pse"),
                    Racun=context.Racuns.FirstOrDefault(x=>x.RacunID==2)
                }
            };
            stavkaracuna.ForEach(p => context.StavkaRacunas.Add(p));
            context.SaveChanges();

            var korisnici = new List<User>()
            {
                new User()
                {
                    Username="Gordan", Password="sha1:64000:18:RYW0mCRmY4BKRdkY25Hc3tXqVxSs8Mk3:WdqGB7KGJNMqJCyMSg36Qv13", isAdmin=true
                },
                new User()
                {
                    Username="Pero", Password="sha1:64000:18:c5DcWgUHMfeo87ue4MvT4YztcwY8bghf:gSTuQK8EiZmEEgncd24LxFvl", isAdmin=false
                }
            
            };
            korisnici.ForEach(p => context.Users.Add(p));
            context.SaveChanges();







            //base.Seed(context); //automatski dodao VisualStudio
        }
    }
}