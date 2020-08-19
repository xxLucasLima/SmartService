import { routes as auth} from '../modules/auth/'
import { routes as usuario} from '../pages/usuario/'
import { routes as produto} from '../pages/produto/'

export default[
    ...auth,
    ...usuario,
    ...produto
] 