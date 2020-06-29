Pristup metodama:

https://localhost:5001/Vinyl/{Naziv rute}

Nazivi GET ruta koje su trenutno dostupne:

-Users (Trenutno je pod zastitom mozes preuzeti ovaj podatak samo ako uzmes token preko https://localhost:5001/Vinyl/Login)
-PublicEvents
-Records
-UserFriends/{Vrednost username-a}
-RecordsFrom/{zanr}
-Caffes
-OwnerCaffes/{Username}
-UserImg/{UserId} (Za ovaj url treba posebna podesavanja da se nameste u kodu kad vam zatreba za testiranje pisite mi.)
-UserPrivateEvents/{Username}
-User/{Username}
-Event/{EventId}
-Record/{RecordId}
-Caffe/{CaffeId}
-CaffeImg/{CaffeId}
-RecordImg/{RecordId}
-InterestedIn/{EventId}
-News
-NewsImgSmall/{NewsId}
-NewsImgLarge/{NewsId}
-UserFavoriteRecords/{UserId}

Nazivi POST ruta koje su trenutno dostupne(Prilikom slanja podatak potrebno je postaviti Content-Type = application/JSON
, a u body proslediti sve neophonde podatke (Primary key je obavezan). Sve sto ne bude prosledjeno deffault ima vrednost null,
 automatski generisani atributi ne moraju biti prosledjeni baza zna sta da radi. Atributi iz modela koji nisu kolone u
bazu(Atributi koji nisu primitivnih tipova ili string-ovi) se ne prosledjuju. U JSON objekat nazivi parametara se moraju 
apsolutno poklapati sa nazivima property-ja u odgovarajuci model.):

-Add/User
-Add/Record
-Add/News
-Add/Event
-Add/Comment
-Add/Caffe
-Add/FavoriteRecord
-Add/Friend (Ako dodate da je User A prijatelj User-a B, dodace se automatski i da je B prijatelj A)
-Add/Interested
-Add/PendingRequest

Za POST-ove nanize mora kroz formu da se prosledi slika i Content-Type mora biti multipart/form-data(Moj property se zove "Image"):

-Add/UserImg/{UserId}
-Add/RecordImg/{RecordId}
-Add/CaffeImg/{CaffeId}
-Add/NewsImgSmall/{NewsId}
-Add/NewsImgLarge/{NewsId}

Vezano za autorizaciju (Kad saljete zahtev API-ma koji imaju zastitu u vidu autorizacije u header ubacujete Authorization = Bearer <Vrednost tokena>):
-Login (Pribavlja token(JWT) property kome se obracate je "token")

Nazivi PUT ruta koje su trenutno dostupne(Vazi sve isto kao i za POST):

-Update/Caffe
-Update/Event
-Update/News
-Update/Record
-Update/User

Nazivi DELETE ruta koje su trenutno dostupne(Kroz URL idu parametri body nije potreban):

-Delete/Caffe/{CaffeId}
-Delete/Comment/{CommentId}
-Delete/Event/{EventId}
-Delete/FavoriteRecord/{UserId}/{RecordId}
-Delete/Friend/{User1Id}/{User2Id} (Brise prijateljstvo i A~B i B~A)
-Delete/Interested/{EventId}/{UserId}
-Delete/News/{NewsId}
-Delete/PendingRequest/{UserSentId}/{UserReceavedId} (Ovaj URL mozete da koristite i za odbijanje zahteva i za otkazivanje
zahteva za prijateljstvo. Samo pazite uvek prvo mora da bude username onog koji je poslao request)
-Delete/Record/{RecordId}
-Delete/User/{UserId}