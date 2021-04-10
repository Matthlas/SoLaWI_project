/*
 Dringlichkeitsstufe 1 sehr hoch - 5 niedrig


pflanzen verschöenern                                       1
In Inventory über Seedlistener Saattütenimages ändern       2
CameraMovement Debuggen (auch weniger sensitiv?)            3

challenges einrichten                                       3
einzelnes Beet entfernen                                    3 
wenn schaufel -> neues beet anlegen                         4
       Idee dazu: inaktive grüne Beetreihen die sich auch so heben, als könnte man mit denen
       interagieren. Wenn Dig() dann werden die aktiv. Wenn man überall Beete erstellen kann
       wird es so unübersichtlich...
aufräumen (v.a. 3rd Party unused löschen)                   4
gießanimation                                               4
gitarre in Hand nehmen                                      5
Feuerknistern                                               5
make day and night slowlier                                 5
waldimage an rändern einfügen?                              5

give plants a collider                                     ? wollen wir das überhaupt machen?



Already done (and explanations):

game logic refinen                                          1       done
ernten debuggen  only if ready to harvest                   1       done
i für instructions                                          1       done
randbedingungen & z !< 0                                    1       done
animation player wieder hinzufügen (ohne jump)              1       done
verschiedene pflanzen in logic                              2       done
        Use SeedListener to add vegetables, use Plant.Transform() to add growing states 
        (switch (vegetable)
            if size == 1 : change zylinder to vegetable)
                            +transform
            if size == 2: transform
            if size == 3: transform
remove vacant spaces in inventory                           2       done
erntezähler                                                 3       done
gitarrenmusik                                               4       done


*/