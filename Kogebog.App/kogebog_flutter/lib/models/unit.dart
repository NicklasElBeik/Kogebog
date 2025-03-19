class Unit {
  final String id;
  final String name;
  final String pluralName;
  final String abbreviation;

  Unit({required this.id, required this.name, required this.pluralName, required this.abbreviation});

  factory Unit.fromJson(Map<String, dynamic> json) {
    return Unit(id: json['id'], name: json['name'], pluralName: json['pluralName'], abbreviation: json['abbreviation']);
  }
}