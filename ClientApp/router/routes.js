import Transactions from 'components/transaction/transaction-home'
import Settings from 'components/settings/settings'

export const routes = [
  { name: 'transaction-home', path: '/', component: Transactions, display: 'Home', icon: 'home' },
  { name: 'manage-settings', path: '/settings', component: Settings, display: 'Settings', icon: 'list' }
]
