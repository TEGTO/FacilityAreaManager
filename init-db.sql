INSERT INTO ProductionFacility (Code, Name, StandardAreaForEquipment)
VALUES 
('PF001', 'Facility A', 5000),
('PF002', 'Facility B', 3000),
('PF003', 'Facility C', 8000);

INSERT INTO ProcessEquipmentType (Code, Name, Area)
VALUES 
('PET001', 'Equipment Type A', 200),
('PET002', 'Equipment Type B', 150),
('PET003', 'Equipment Type C', 300);

INSERT INTO EquipmentPlacementContract (Code, ProductionFacilityCode, ProcessEquipmentTypeCode, NumberOfEquipmentUnits)
VALUES 
('EPC001', 'PF001', 'PET001', 10), 
('EPC002', 'PF001', 'PET002', 15),
('EPC003', 'PF002', 'PET003', 5), 
('EPC004', 'PF003', 'PET001', 8), 
('EPC005', 'PF003', 'PET002', 12); 
