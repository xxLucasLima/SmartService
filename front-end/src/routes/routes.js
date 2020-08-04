import { routes as auth} from '../modules/auth/'
import { routes as usuario} from '../pages/usuario/'

export default[
    ...auth,
    ...usuario
] 