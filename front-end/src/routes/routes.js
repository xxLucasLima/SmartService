import { routes as auth} from '../modules/auth/'
import { routes as usuario} from '../pages/usuario/'
import { routes as produto} from '../pages/produto/'
import { routes as lote} from '../pages/lote/'
import { routes as cliente} from '../pages/cliente/'
import { routes as empresa} from '../pages/empresa/'

export default[
    ...auth,
    ...usuario,
    ...produto,
    ...lote,
    ...cliente,
    ...empresa
] 