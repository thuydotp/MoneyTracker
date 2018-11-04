import CounterExample from 'components/counter-example'
import FetchData from 'components/fetch-data'
import HomePage from 'components/home-page'
import SpendingSummary from 'components/spending-summary'
import SpendingChange from 'components/spending-change'

export const routes = [
  { name: 'home', path: '/', component: HomePage, display: 'Home', icon: 'home' },
  { name: 'counter', path: '/counter', component: CounterExample, display: 'Counter', icon: 'graduation-cap' },
  { name: 'fetch-data', path: '/fetch-data', component: FetchData, display: 'Fetch data', icon: 'list' },
  { name: 'spending-summary', path: '/spending-summary', component: SpendingSummary, display: 'Spending Summary', icon: 'list' },
  { name: 'spending-change', path: '/spending-change', component: SpendingChange, display: 'Spending Change', icon: 'list' }
]
