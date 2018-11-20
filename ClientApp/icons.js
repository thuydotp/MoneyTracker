import fontawesome from '@fortawesome/fontawesome'
// Official documentation available at: https://github.com/FortAwesome/vue-fontawesome
import FontAwesomeIcon from '@fortawesome/vue-fontawesome'

// If not present, it won't be visible within the application. Considering that you
// don't want all the icons for no reason. This is a good way to avoid importing too many
// unnecessary things.
fontawesome.library.add(
  // Brands
  require('@fortawesome/fontawesome-free-brands/faFontAwesome'),
  require('@fortawesome/fontawesome-free-brands/faMicrosoft'),
  require('@fortawesome/fontawesome-free-brands/faVuejs')
)

const SupportedIcons = [
  'faEnvelope',
  'faGraduationCap',
  'faList',
  'faSpinner',
  'faTag',
  'faCoffee',
  'faTshirt',
  'faPhone',
  'faShoppingBag',
  'faMobileAlt',
  'faUmbrella',
  'faUtensilSpoon',
  'faUtensils',
  'faGlassMartini',
  'faHome',
  'faCut',
  'faSchool',
  'faHospitalAlt',
  'faFilm',
  'faFootballBall',
  'faTaxi',
  'faSubway',
  'faMotorcycle',
  'faCar',
  'faHandHoldingUsd',
  'faGem',
  'faMoneyCheck',
  'faCreditCard',
  'faMoneyBill',
  'faDollarSign',
  'faYenSign',
  'faEuroSign',
  'faPiggyBank',
  'faGasPump',
  'faCoins',
  'faBeer'
]

SupportedIcons.forEach(iconName => {
  fontawesome.library.add(
    require('@fortawesome/fontawesome-free-solid/' + iconName)
  )
})

const FixedIcons = {
  camelToKebab (string) {
    return string.replace(/([a-z])([A-Z])/g, '$1-$2').toLowerCase()
  },
  getIconNames () {
    let numberOfRemoveLetter = 'fa'.length
    return SupportedIcons.map(x => this.camelToKebab(x.slice(numberOfRemoveLetter)))
  }
}

export {
  FontAwesomeIcon,
  FixedIcons
}
