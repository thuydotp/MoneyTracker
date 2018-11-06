import CounterExample from 'components/counter-example'
import FetchData from 'components/fetch-data'
import HomePage from 'components/home-page'
import SpendingSummary from 'components/spending-summary'
import SpendingChange from 'components/spending-change'
import SpendingAccount from 'components/spending-account'
import Icon from 'components/icon'
import SpendingCategory from 'components/spending-category'

export const routes = [
  { name: 'home', path: '/', component: HomePage, display: 'Home', icon: 'home' },
  { name: 'counter', path: '/counter', component: CounterExample, display: 'Counter', icon: 'graduation-cap' },
  { name: 'fetch-data', path: '/fetch-data', component: FetchData, display: 'Fetch data', icon: 'list' },
  { name: 'spending-summary', path: '/spending-summary', component: SpendingSummary, display: 'Spending Summary', icon: 'list' },
  { name: 'spending-change', path: '/spending-change', component: SpendingChange, display: 'Spending Change', icon: 'list' },
  { name: 'spending-account', path: '/spending-account', component: SpendingAccount, display: 'Spending Account', icon: 'list' },
  { name: 'icon', path: '/icon', component: Icon, display: 'Icon', icon: 'list' },
  { name: 'spending-category', path: '/spending-category', component: SpendingCategory, display: 'Spending Category', icon: 'list' }
]
