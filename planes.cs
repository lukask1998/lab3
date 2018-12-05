public final class Planes implements KTUable {

    // bendri duomenys visiems automobiliams (visai klasei)
    private static final int minDate = 1900;
    private static final int nowDate = LocalDate.now().getYear();
    private static final double minFuelCap = 100.0;
    private static final double maxFuelCap = 333000.0;
    private String name = "";
    private String category = "";
    private int date = -1;
    private int fuelCap = -1;

    public Planes() {
    }

    public Planes(String name, String category,
            int date, int fuelCap) {
        this.name = name;
        this.category = category;
        this.date = date;
        this.fuelCap = fuelCap;
        validate();
    }

    public Planes(String e) {
        this.parse(e);
    }

    public Planes(Builder builder) {
        this.name = builder.name;
        this.category = builder.category;
        this.date = builder.date;
        this.fuelCap = builder.fuelCap;
        validate();
    }

    @Override
    public Planes create(String dataString) {
        return new Planes(dataString);
    }

    @Override
    public String validate() {
        String klaidosTipas = "";
        if (date < minDate || date > nowDate) {
            klaidosTipas = "Invalid date ["
                    + minDate + ":" + nowDate + "]";
        }
        if (fuelCap < minFuelCap || fuelCap > maxFuelCap) {
            klaidosTipas += "Invalid fuel capacity [" + minFuelCap
                    + ":" + maxFuelCap + "]";
        }
        return klaidosTipas;
    }

    @Override
    public void parse(String dataString) {
        try {   // ed - tai elementarūs duomenys, atskirti tarpais
            Scanner ed = new Scanner(dataString);
            name = ed.next();
            category = ed.next();
            date = ed.nextInt();
            setFuelCap(ed.nextInt());
            validate();
        } catch (InputMismatchException e) {
            Ks.ern("Invalid formatting about plane -> " + dataString);
        } catch (NoSuchElementException e) {
            Ks.ern("Not enough information about plane -> " + dataString);
        }
    }

    @Override
    public String toString() {  // papildyta su autoRegNr
        return name + " " + category + " " + date + " " + getFuelCap();
    }

    public String getName() {
        return name;
    }

    public String getCategory() {
        return category;
    }

    public int getDate() {
        return date;
    }

    public int getFuelCap() {
        return fuelCap;
    }

    public void setFuelCap(int fuelCap) {
        this.fuelCap = fuelCap;
    }

    @Override
    public int hashCode() {
        return Objects.hash(name, category, date, fuelCap);
    }

    @Override
    public boolean equals(Object obj) {
        if (obj == null) {
            return false;
        }
        if (getClass() != obj.getClass()) {
            return false;
        }
        final Planes other = (Planes) obj;
        if (!Objects.equals(this.name, other.name)) {
            return false;
        }
        if (!Objects.equals(this.category, other.category)) {
            return false;
        }
        if (this.date != other.date) {
            return false;
        }
        if (this.fuelCap != other.fuelCap) {
            return false;
        }

        return true;
    }

    // Lektuvu klases objektų gamintojas
    public static class Builder {

        private final static Random RANDOM = new Random(1949);  // Atsitiktinių generatorius
        private final static String[][] CATEGORY = { // galimų automobilių markių ir jų modelių masyvas
            {"Airco", "Transport", "General", "Helicopters"},
            {"ATR", "Research", "Transport"},
            {"Junkers", "Transport"},
            {"Bleriot", "Transport", "Helicopters"},
            {"Avro", "Research", "Transport"},
            {"Aero", "Research", "Transport"},
            {"Bell", "Transport", "General", "Helicopters"},
            {"ACME", "Transport"},
            {"Albatros", "Transport", "General", "Helicopters"},
        };

        private String name = "";
        private String category = "";
        private int date = -1;
        private int fuelCap = -1;

        public Planes build() {
            return new Planes(this);
        }

        public Planes buildRandom() {
            int na = RANDOM.nextInt(CATEGORY.length);        // markės indeksas  0..
            int ca = RANDOM.nextInt(CATEGORY[na].length - 1) + 1;// modelio indeksas 1..              
            return new Planes(CATEGORY[na][0],
                    CATEGORY[na][ca],
                    1900 + RANDOM.nextInt(90),
                    10000 + RANDOM.nextInt(220000));
        }

        public Builder date(int date) {
            this.date = date;
            return this;
        }

        public Builder name(String name) {
            this.name = name;
            return this;
        }

        public Builder category(String category) {
            this.category = category;
            return this;
        }

        public Builder fuelCap(int fuelCap) {
            this.fuelCap = fuelCap;
            return this;
        }
    }
}
