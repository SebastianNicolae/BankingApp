# BankingApp


Banking App which simulates transactions between accounts.


TO DO 
- deal with Status and Currency in a proper manner ( separate tables) and implementation
- apply pagination
- analyze and create more dtos for some endpoints
- implement more validations
- analyze an authentification addition ( maybe jwt ? )
- add some more swagger features


Transaction Payload example : 

 "payerAccount": "1231-2121",
  "receiverAccount": "2212-1212",
  "iban": "RO49AAAA1B31007593840000",
  "amount": 10,
  "currency": "RON"

  Note - Happy scenario has been taken into consideration, where we do not have null properties and objects
