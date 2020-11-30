

## Arquitetura

Foi utilizado como base do densenvolvimento uma arquitetura baseada em 
DDD (Domain Driver Design), com o objetivo de criações de diversas camadas 
com niveis de abstrações diferentes, na qual cada uma desempenha um papel
específico para compor a solução como todo.

Também foi utilizado o Mediator na solução para proporcionar um maior
desacoplamento entre os objetos e camadas.

## Solução

O desafio consistem em validar password baseado em algumas premissas:

* Nove ou mais caracteres
* Ao menos 1 dígito
* Ao menos 1 letra minúscula
* Ao menos 1 letra maiúscula
* Ao menos 1 caracter especial (!@#$%^&*()-+)
* Não possuir caracter repetidos (Considerei que 'a' e 'A' não são caracteres repetidos,
pois na tabela ascii possui um mapeamento diferente entre maiusculas e minusculas)
* Espaço em branco não é considerado como caracter válido

Basicamente a solução consiste num loop com um boolean correspondente a cada regra.
Cada vez que completa uma regra, a senha ganha um score. Uma regra completa não é reanalisada,
visto que ela já foi atendida.

É considerado uma senha forte caso tenha um score igual a 3: especial (+1), digito (+1),
tem maisucula e minuscula (+1).

Caso seja encontrado em algum momento espaço, a senha é invalidada. Da mesma forma segue para a quantidade mínima de caracteres.